<#
	.SYNOPSIS
	A class to wrap around WSUS api to simplyfy use

	.NOTES
	Unfinished/Abandoned
#>

Class WSUS {

    [String]$Server

    [Int]$Port

    [Bool]$SecureConnection

    WSUS ([String]$Server, [Int]$Port, [Bool]$SecureConnection) {
        
        #d$ErrorActionPreference = 'Stop'
        $this.Server           = $Server
        $this.Port             = $Port
        $this.SecureConnection = $SecureConnection
        #$UpdateServicesDLL    = "$env:SystemRoot\Microsoft.Net\assembly\GAC_MSIL\Microsoft.UpdateServices.Administration\v4.0_4.0.0.0__31bf3856ad364e35\Microsoft.UpdateServices.Administration.dll"
        
        #If (Test-Path -Path $UpdateServicesDLL){
        Try {
            Write-Verbose "Loading Required Assemblies..."
            #Write-Verbose "Loading $UpdateServicesDLL"
            #[Reflection.assembly]::LoadFile($UpdateServicesDLL) 
            Add-Type -AssemblyName 'Microsoft.UpdateServices.Administration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35' -ErrorAction Stop
        }
        Catch {
            Write-Warning "Cannot Find Required Assembly: Microsoft.UpdateServices.Administration..."
            Write-Warning 'Try Running "Install-WindowsFeature -Name UpdateServices-RSAT"'
            Throw $_
        }
        #}
        #Else {

         #   Write-Error "Cannot find $UpdateServicesDLL"
            #$PSCmdlet.ThrowTerminatingError(
            #    [System.Management.Automation.ErrorRecord]::new(
            #        ([Exception]::new('FileNotFound')),
            #        "Cannot find $UpdateServicesDLL",
            #        ([System.Management.Automation.ErrorCategory]::ObjectNotFound),
            #        $UpdateServicesDLL
            #    )
            #)
        #}
        #[Microsoft.UpdateServices.Administration.AdminProxy]::GetUpdateServer($ComputerName,$UseSecureConnection,$PortNumber)
    }
}