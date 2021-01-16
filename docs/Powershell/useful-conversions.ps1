<#
    .NOTES
    converting from hex
#>
$hex = 0x1F4A9

[int]$hex # integer
[System.BitConverter]::GetBytes($hex) # bytes
[byte]0xFF # can only convert single byte this way

<#
    .NOTES
    converting from bytes
#>
$bytes = 169, 244, 1, 0

# beware endianess hwne converting to hex
$convertedHex = [System.BitConverter]::ToString($bytes)
if ([System.BitConverter]::IsLittleEndian) {
    [Array]::Reverse($convertedHex)
}
