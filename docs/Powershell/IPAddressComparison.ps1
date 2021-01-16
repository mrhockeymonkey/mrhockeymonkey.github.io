#Define the Networks you wish to check
$Networks = @(
    '192.168.1.0/8'
    '172.16.1.0/16'
)

#Create Objects for each network that define the IP range as a decimal
$PossibleSubnets = $Networks | ForEach-Object {
     $NetworkAddress = [IPAddress]($_ -split '/')[0]
     $MaskLength = [Byte]($_ -split '/')[1]
     
     $NetworkAddressBytes = $NetworkAddress.GetAddressBytes()
     [Array]::Reverse($NetworkAddressBytes)
     $RangeLower = [BitConverter]::ToUInt32($NetworkAddressBytes, 0)
     
     $DecimalMask = [Convert]::ToUInt32(("1" * $MaskLength).PadRight(32, "0"), 2)
     $RangeUpper = $RangeLower -bor -bnot $DecimalMask
       
    [PSCustomObject]@{
        Network = $_
        NetworkAddress = $NetworkAddress
        MaskLength = $MaskLength
        RangeLower = $RangeLower
        RangeUpper = $RangeUpper
    }
}


'localhost' | ForEach-Object {
    Write-Host "Checking $_" -ForegroundColor Yellow
    $ComputerName = $_
    $Interfaces = [Net.NetworkInformation.NetworkInterface]::GetAllNetworkInterfaces()
    $Interfaces | ForEach-Object {
        $Interface = $_
        $IpAddresses =  $_.GetIPProperties().UnicastAddresses | Where-Object { $_.Address.ToString() -notin '127.0.0.1', '::1' } | Select-Object -ExpandProperty Address
        $IPAddresses | ForEach-Object {
            #Here we convert the IP addresses found to Decimal for comparison
            $IPAddressBytes = $_.GetAddressBytes()
            [Array]::Reverse($IPAddressBytes)
            $DecimalIPAddress = [BitConverter]::ToUInt32($IPAddressBytes, 0)

            $MySubnets = $PossibleSubnets| Where-Object {$DecimalIPAddress -ge $_.RangeLower -and $DecimalIPAddress -le $_.RangeUpper}
            
            [PSCustomObject]@{
                ComputerName = $ComputerName
                Interface = $Interface.Name
                Network = $MySubnets.Network
            }
        }
    }
}