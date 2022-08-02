<#
    The Purpose of this Script is to setup the WinPE environment ready for the Build Process to Start
    This Includes Getting Credentials, Localization, Mapping a Drive to the Local Build Server and Invoking PSake
#>
[CmdletBinding()]
Param()

$SearchScopeRoot    = 'LDAP://'
$SearchScopeSubnets = 'LDAP://'
$MapDriveLetter     = 'Z'
$ShareName          = 'OSD'
$SrcPSakeModule     = "${MapDriveLetter}:\Control\Tools\PSake"
$SrcStartScript     = ""
$DestPSakeModule    = 'X:\Tools\PSake'
$DestStartScript    = 'X:\Tools\'


#Setup Shell Attributes
$ShellSettings = (Get-Host).UI.RawUI
$ShellSettings.WindowTitle = "The Build Mallard"
$NewBufferSize = $ShellSettings.BufferSize
$NewBufferSize.Height = 300
$NewBufferSize.Width = 100
$ShellSettings.BufferSize = $NewBufferSize
$NewWindowSize = $ShellSettings.WindowSize
$NewWindowSize.Height = 30
$NewWindowSize.Width = 100
$ShellSettings.WindowSize = $NewWindowSize


#Setup Credentials
$Username = ''
$Password = '' | ConvertTo-SecureString -AsPlainText -Force
$Credential = New-Object System.Management.Automation.PSCredential -ArgumentList $Username,$Password

#Connect to AD to find Subnets
Write-Host "Connecting to AD Sites and Services..."
Try {
    $DirectoryEntrySubnets = New-Object System.DirectoryServices.DirectoryEntry $SearchScopeSubnets, $Credential.UserName, $Credential.GetNetworkCredential().Password, 'Secure' 
    
    #Get all the Subnet Information and Parse it into a Custom Object
    $SubnetsFromAD = $DirectoryEntrySubnets.Children | Select Name,SiteObject | ForEach-Object {
        
        $SiteName            = ($_.SiteObject -split ',')[0] -replace 'CN=',''
        $NetworkAddress      = [IPAddress]($_.Name -split '/')[0]
        $MaskLength          = [Byte]($_.Name -split '/')[1]
        $NetworkAddressBytes = $NetworkAddress.GetAddressBytes()
        
        [Array]::Reverse($NetworkAddressBytes)
        
        $RangeLower  = [BitConverter]::ToUInt32($NetworkAddressBytes, 0)
        $DecimalMask = [Convert]::ToUInt32(("1" * $MaskLength).PadRight(32, "0"), 2)
        $RangeUpper  = $RangeLower -bor -bnot $DecimalMask  
        
        
        [PSCustomObject]@{
            SiteName       = $SiteName
            NetworkAddress = $NetworkAddress
            MaskLength     = $MaskLength
            RangeUpper     = $RangeUpper
            RangeLower     = $RangeLower
        }
    }
}
Catch {
    $PSCmdlet.ThrowTerminatingError($_)
}


#Calculate Local Subnet
Write-Host "Calculating Local Subnet Info..."
Try{
    #Retreive the DHCP IP leased to this WinPE
    $IPAddress = [Net.NetworkInformation.NetworkInterface]::GetAllNetworkInterfaces() | 
        ForEach-Object {
            $_.GetIPProperties().UnicastAddresses
        } |
        Where-Object { $_.Address.ToString() -notin '127.0.0.1', '::1' } |
        Select-Object -ExpandProperty Address

    #Test this IP a return any matching subnets
    $ValidSubnets = @()
    $IPAddress | ForEach-Object {
        $IPAddressBytes = $_.GetAddressBytes()
        [Array]::Reverse($IPAddressBytes)
        $DecimalIPAddress = [BitConverter]::ToUInt32($IPAddressBytes, 0)

        $ValidSubnets += $SubnetsFromAD | Where-Object {$DecimalIPAddress -ge $_.RangeLower -and $DecimalIPAddress -le $_.RangeUpper} | Select -First 1
    }
    Write-Verbose "Found $($ValidSubnets.Count) Valid Subnets"
    $MySubnets = $ValidSubnets | Group-Object -Property SiteName

    If ($MySubnets.Name.Count -eq 1) {
        Write-Verbose "Valid Subnets are the same ($($MySubnets.Name))"
        $Site = $MySubnets.Name
        $Pad = 10

        Write-Host "Site: $Site"
        
            $LocalShare = "fs-infra.$Site.sites.uberit.net"
            #$LocalShareIp = Test-Connection $LocalShare -Count 1 | Select -ExpandProperty IPV4Address

            Write-Host "LocalShare: $LocalShare"

        }
    }
    ElseIf ($MySubnets.Name.Count -gt 1) {
        Write-Verbose "Valid Subnets are different! ($($MySubnets.Name -join ','))"
        Write-Error "Found Multiple Possible Subnets!" -ErrorAction Stop
    }
    Else {
        Write-Error "Failed to find a valid subnet! Check AD Sites And Services" -ErrorAction Stop
    }

}
Catch {
    $PSCmdlet.ThrowTerminatingError($_)
}


#Map a Drive to that build server
Try {
    #Remove any Exisiting mapps to Z
    If (Get-PSDrive -Name $MapDriveLetter -ErrorAction SilentlyContinue) {
        Remove-PSDrive -Name $MapDriveLetter -Force
    }
    
    Write-Host "Mapping ${MapDriveLetter}:\ to \\$LocalShare\$ShareName..." 
    New-PSDrive -Name $MapDriveLetter -PSProvider FileSystem -Root "\\$LocalShare\$ShareName" -Credential $Credential -Persist -Scope Global -ErrorAction Stop | Out-Null
}
Catch {
    $PSCmdlet.ThrowTerminatingError($_)
}


#Invoke PSake
Write-Host "`nQuack Quack...`n"
Try {
    Remove-Item -Path $DestPSakeModule -Recurse -Force -ErrorAction SilentlyContinue
    Copy-Item -Path $SrcPSakeModule -Destination $DestPSakeModule -Recurse -Force -ErrorAction Stop
    Copy-Item -Path $SrcStartScript -Destination $DestStartScript -Force -ErrorAction Stop

    Import-Module $DestPSakeModule -Force
    Invoke-psake -buildFile $DestStartScript -nologo -parameters @{
        Username = $Credential.UserName
        SecPassword = $Credential.GetNetworkCredential().SecurePassword
        Site = $Site
    }
}
Catch {
    $PSCmdlet.ThrowTerminatingError($_)
}


#Le Finn

