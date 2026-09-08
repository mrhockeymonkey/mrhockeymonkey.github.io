function Get-ADLocalSiteInfo {
    # .SYNOPSIS
    #   Gets site information from active directory sites and services based on your subnet.
    # .DESCRIPTION
    #   Get-ADLocalSiteInfo uses the IP addresses assigned to the local host (or specified IP) and compares it to information in AD sites and Services
    # .INPUTS
    #   System.Net.IPAddress
    # .OUTPUTS
    #   PSCustomObject
    # .EXAMPLE
    #   Get-ADLocalSiteInfo
    # .EXAMPLE
    #   Get-ADLocalSiteInfo -IPAddress 172.16.1.100
    # .NOTES
    #   Author: Chris Dent / Scott Matthews
    [CmdletBinding()]
    Param (
        [IPAddress[]]$IPAddress
    )
    #If we are not given a specific IP address then retrieve the local host IP addresses
    if(-not $PSBoundParameters.ContainsKey('IPAddress')){
        Try{
            $IPAddress = [Net.NetworkInformation.NetworkInterface]::GetAllNetworkInterfaces() | 
                ForEach-Object {$_.GetIPProperties().UnicastAddresses} |
                Where-Object {$_.Address.ToString() -notin '127.0.0.1', '::1'} |
                Select-Object -ExpandProperty Address
            
                Write-Verbose "Found the following IP Addresses: $($IPAddress -join ', ')"
        }
        Catch {
            Write-Warning "Failed to get host IP address..."
   
            Write-Warning "Exception Type: $($_.Exception.GetType().FullName)" 
   
            Write-Warning "Exception Message: $($_.Exception.Message)" 
        }
    }

    #Get all the subnets listed from sites and services and create an object to be used for comparison
    Try {
        $Sites = [System.DirectoryServices.ActiveDirectory.Forest]::GetCurrentForest().Sites
        $SubnetsToCheck = $Sites | ForEach-Object {
            $Site = $_
            $_.Subnets | ForEach-Object {
                [PSCustomObject]@{
                    Site = $Site.Name
                    Range = $_
                    NetworkAddress = [IPAddress]($_ -split '/')[0]
                    MaskLength = [Byte]($_ -split '/')[1]
                    RangeLower = $null
                    RangeUpper = $null
                }
            }
        }

        #Now we calculate the RangeLower and RangeUpper for each subnet as decimal representations
        $SubnetsToCheck | ForEach-Object {
            $NetworkAddressBytes = $_.NetworkAddress.GetAddressBytes()
            [Array]::Reverse($NetworkAddressBytes)
            $_.RangeLower = [BitConverter]::ToUInt32($NetworkAddressBytes, 0)
            
            $DecimalMask = [Convert]::ToUInt32(("1" * $_.MaskLength).PadRight(32, "0"), 2)
            $_.RangeUpper = $_.RangeLower -bor -bnot $DecimalMask  
        }

        Write-Verbose "Found $($SubnetsToCheck.count) subnets in sites and Services"
    }
    Catch {
        Write-Warning "Failed to get subnets from AD Sites and Services"
        Write-Warning "Exception Type: $($_.Exception.GetType().FullName)" 
        Write-Warning "Exception Message: $($_.Exception.Message)" 
        Write-Warning "Exception Message: $($_.Exception)" 
    }


    #Now search through and pick out the site that matches the host IP
    $IPAddress | ForEach-Object{
        Write-Verbose "Searching for a match for $($_.IPAddressToString)"
        
        #Here we convert the IP addresses found to Decimal for comparison
        $IPAddressBytes = $_.GetAddressBytes()
        [Array]::Reverse($IPAddressBytes)
        $DecimalIPAddress = [BitConverter]::ToUInt32($IPAddressBytes, 0)

        $SubnetsToCheck | Where-Object {$DecimalIPAddress -ge $_.RangeLower -and $DecimalIPAddress -le $_.RangeUpper}
    }
}
