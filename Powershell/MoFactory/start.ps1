<#
.SYNOPSIS
SysComp Build (multi-threaded)

.DESCRIPTION
This build script is resposible for compiling .mof files for all SysComp nodes. 
1) It will generate DSC configurations based on the current roles/profiles as defined in /Roles
2) It will query SysComp API and convert this information into standard format for DSC ConfigurationData
3) It will create an InitialSessionState and pre-import these generated DSC configurations
4) Using runspaces it will invoke FinalConfig for each node resulting in the .mof artifact required
#>

Param (
$Environment = 'PROD',
$MaxActiveRunspaces = 12
)

#Properties
$Root         = Join-Path -Path $PSScriptRoot -ChildPath '..\..'
$BuildDir     = Join-Path -Path $Root -ChildPath "Generated"
$RolesDir     = Join-Path -Path $BuildDir -ChildPath "Roles"
$MofsDir      = Join-Path -Path $BuildDir -ChildPath "Mofs"
$ConfigsDir   = Join-Path -Path $Root -ChildPath "Production\Modules"
$ResourcesDir = Join-Path -Path $Root -ChildPath "Production\Resources"

$DscTemplate = @"
Configuration {0} {{
Param ({1})

{2}
}}
"@

Task . Init, ConvertRolesToDscConfigurations, GetConfigurationData, CreateSessionState, CompileMofs

<#
.NOTES (Helper Functions)
Below are a series of helper functions for this InvokeBuild script. Potentially they need to be moved to a seperate file/module
#>


<#
.SYNOPSIS
Output the information required to create a DSC configuration for a role (recursively)
#>

Function Resolve-Role {
Param (
$Role,
$Parent
)
#Return the required information
$RoleName = $Role.Name -Replace " ",""

[PSCustomObject]@{
RoleName = $RoleName
ParentRole = $Parent -Replace " ",""
Profiles = $Role.Profiles 
}

#Test for existence of child roles and call the function again for each (Recurse)
If (($Role.Children).Count -gt 0) {
ForEach ($ChildRole in $Role.Children) {
Resolve-Role -Role $ChildRole -Parent $RoleName
}
}
}



<#
.SYNOPSIS
Convert the Roles\*.json into DSC configuration blocks
#>

Function ConvertTo-DscConfiguration {
Param (
[Parameter(Mandatory, ValueFromPipeline)]$Path
)
Begin {}
Process {
#Read in json data
$RoleGroup = Get-Content -Path $_ -Raw | ConvertFrom-Json 
$RoleGroupName = $RoleGroup.Name -Replace " ",""
#Recursively resolve each role and child role to get the details we need
ForEach ($Role in $RoleGroup.Roles) {
$RoleDetails = Resolve-Role -Role $Role
#Each detail represents a role and we need to generate a dsc configuration file per role
ForEach ($Detail in $RoleDetails) {

$Body = New-Object -TypeName System.Collections.ArrayList
#If there is a parent role we call this configuration. This is how roles inherit configurations from above (in a chain)
If ($Detail.ParentRole) {
$Body.Add("${RoleGroupName}_$($Detail.ParentRole)_Role 'ParentRole' -Node `$Node -Context `$Context") | Out-Null
}

#Add each profile that has been assigned to this role
ForEach ($Profile in $Detail.Profiles) {
$Body.Add("$Profile `'$Profile`' -Node `$Node -Context `$Context") | Out-Null
}

#Format the DscTemplate and write to file. 
$RoleName = "${RoleGroupName}_$($Detail.RoleName)_Role"
$Value = $DscTemplate -f $RoleName, '$Node, $Context', $($Body -join "`n`t")
#$Folder = New-Item -Path $RolesDir -Name $RoleName -ItemType Directory
New-Item -Path $RolesDir -Name "$RoleName.psm1" -ItemType File -Value $Value
}
}
}

End{}
}






<#
.SYNOPSIS
Split the standard DSC configuration data into individual nodes
#>

Function Split-ConfigurationData {
    Param (
        [Parameter(Mandatory, ValueFromPipeline)]
        $InputObject
    )

    Begin {}

    Process {
        ForEach ($Node in $InputObject.AllNodes) {
            $ConfigData = @{
       AllNodes = [Array]$Node
       }

            Write-Output $ConfigData
        }
    }

    End {}

}

<#
.NOTES (Task Definitions)
Below are the InvokeBuild task definitions
#>

Task Init {
#Refresh BuildDir
Get-Item -Path $BuildDir -ErrorAction SilentlyContinue | Remove-Item -Force -Recurse
New-Item -Path $BuildDir -ItemType Directory
New-Item -Path $RolesDir -ItemType Directory
New-Item -Path $MofsDir -ItemType Directory
}

Task ConvertRolesToDscConfigurations {
#Take roles as defined in json and create dsc configuration blocks
Join-Path -Path $Root -ChildPath "Roles\$Environment" | Get-ChildItem | Select-Object -ExpandProperty FullName | ConvertTo-DscConfiguration
}

Task CreateSessionState {
$Script:SessionState = [System.Management.Automation.Runspaces.InitialSessionState]::CreateDefault2()
Get-ChildItem -Path $RolesDir -Filter *.psm1 | Select-Object -ExpandProperty FullName | ForEach-Object {
$SessionState.ImportPSModule($_)
}
<#
.NOTES (Weirdness)
it would make sense here to also pre-import configurations but for some reason this is really flakey
Instead configurations are imported at runtime :'(
#>

Write-Output "InitialSessionState created: `n"
Write-Output $SessionState

Write-Output "Imported modules are: `n"
Write-Output $SessionState.Modules.Name
}

Task GetConfigurationData {
$Script:ConfigurationData = Get-SysCompNodeData | ConvertTo-DscConfigurationData
$Script:ConfigurationData
}

Task CompileMofs {

#Create runspace pool and var to track runspaces
$Runspaces = [System.Collections.ArrayList]::New()
$RunspacePool = [RunspaceFactory]::CreateRunspacePool(
1, #Min runspaces
$MaxActiveRunspaces, #Max runspaces
$SessionState,
$Host
)
$RunspacePool.Open()

#Create a powershell instance for each mof we need to build and invoke
$Script:ConfigurationData | Split-ConfigurationData | ForEach-Object {

$Params = @{
ConfigData    = $_
OutputPath    = $MofsDir
ResourcesPath = $ResourcesDir
RolesPath     = $RolesDir
ConfigsPath   = $ConfigsDir
}
$Script = {
Param ($ConfigData, $OutputPath, $ResourcesPath, $RolesPath, $ConfigsPath)

#Update PSModulePath so that we only build using resources in repo
$env:PSModulePath = "C:\Windows\system32\WindowsPowerShell\v1.0\Modules;$ResourcesPath"

#Import configurations (Note: Roles were pre imported when we created an InitialSessionState)
Get-ChildItem -Path $ConfigsPath -Filter '*.psm1' | ForEach-Object {
Import-Module $_.FullName -Force -ErrorAction Stop 
}

Configuration FinalConfig {
Node $AllNodes.NodeName {
ForEach ($Role in $Node.Roles){
$roleSafeName = $role.RoleGroup + '_' + $role.Role
& "${roleSafeName}_Role" "${roleSafeName}_NAME" -Node $Node -Context $Context
}
}
}

FinalConfig -ConfigurationData $ConfigData -OutputPath $OutputPath
}

$PowerShell = [System.Management.Automation.PowerShell]::Create()
$PowerShell.RunspacePool = $RunspacePool
[Void]$PowerShell.AddScript($Script).AddParameters($Params)

[Void]$Runspaces.Add([PSCustomObject]@{
Node = $_.AllNodes.NodeName
Runspace = $PowerShell.BeginInvoke()
PowerShell = $PowerShell
})
}

#Track progress and retreive output/errors
Do {
$More = $false
$CompletedRunspaces = [System.Collections.ArrayList]::New()
$Runspaces | Where-Object -FilterScript {$_.Runspace.isCompleted} | ForEach-Object {
$Node = $_.Node
$InnerError = $_.Powershell.Streams.Error
Try {
$_.Powershell.EndInvoke($_.Runspace)
}
Catch {
Write-Warning "An error has occurred, tearing down runspace pool!"
$RunspacePool.Close()
$RunspacePool.Dispose()

Write-Error "Error compiling mof for $($Node) - $InnerError"
Write-Error $_
}
Finally {
$_.PowerShell.Dispose()
[Void]$CompletedRunspaces.Add($_)
}
}
$CompletedRunspaces | ForEach-Object {
[Void]$Runspaces.Remove($_)
}
If ($Runspaces.Count -gt 0) {
$More = $true
Start-Sleep -Seconds 1
}
}
While ($More)

$RunspacePool.Close()
$RunspacePool.Dispose()

}

