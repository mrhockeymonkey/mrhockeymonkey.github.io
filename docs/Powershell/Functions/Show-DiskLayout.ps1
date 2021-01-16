<#
	.SYNOPSIS
	Show graphically in the shell the current or proposed disk layout

	.NOTES
	A little broken. Erros depend greatly on the shell width and what disk config is
#>

Function Show-DiskLayout {
    [CmdletBinding(DefaultParameterSetName = 'CurrentLayout')]
    Param (
        
        [Parameter(Mandatory = $false, ParameterSetName='CustomLayout')]
        [PSObject]$CustomLayout,

        [Parameter(Mandatory = $false)]
        #[ValidateRange(100,9999)]
        [Int]$Width
    )
    
    #Check to see if we are using a custom width or the width of the UI
    If (-not $PSBoundParameters.ContainsKey('Width')) {
        $Width  = $Host.UI.RawUI.WindowSize.Width -3
    }
    Write-Debug "Width = $Width"
    Write-Debug "ParamSetName = $($PSCmdlet.ParameterSetName)"

    If ($PSCmdlet.ParameterSetName -eq 'CustomLayout') {
        Write-Verbose "Using Custom Disk Config..." 
        $DiskConfig = $CustomLayout
    }
    ElseIf ($PSCmdlet.ParameterSetName -eq 'CurrentLayout') {
        Write-Verbose "Querying current disk config..."
        $DiskConfig = Get-Disk | ForEach-Object {
            $ThisDisk = $_
            $Partitions = @()
            $_ | Get-Partition | ForEach-Object {
                $Partitions += [PSCustomObject]@{
                    Name = $_ | Get-Volume | Select -ExpandProperty FileSystemLabel
                    Size = $_.Size / 1GB
                }
            }
            [PSCustomObject]@{
                DiskNo = $ThisDisk.Number
                DiskSize = $ThisDisk.Size
                Partitions = $Partitions
            }
        }
    }
    
    
    
    $DiskConfig | %{
        $ThisDiskSize = $_.DiskSize
        $Remaining = 1

        #Create String Builders for each line
        ($Line1 = New-Object System.Text.StringBuilder).Append('|') | Out-Null
        ($Line2 = New-Object System.Text.StringBuilder).Append('|') | Out-Null
        ($Line3 = New-Object System.Text.StringBuilder).Append('|') | Out-Null
        ($Line4 = New-Object System.Text.StringBuilder).Append('|') | Out-Null

        #Calculate how big each partition will be when displayed, respecting the minimum space required by each
        $PartSizes = $_.Partitions | ForEach-Object {
            Write-Debug "Partition Name: $($_.Name)"
            
            $ThisName = If ([String]::IsNullOrEmpty($_.Name)){'<No Label>'}Else{$_.Name}
            $ThisNameLength = $ThisName.Length + 3 # +3 here accounts for a leading ' ' and a trailing ' |'
            Write-Debug "Name Label Length = $ThisNameLength"
            
            $ThisSize = "$([Math]::Round(($_.Size),2)) GB"
            $ThisSizeLength = $ThisName.Length + 3
            Write-Debug "Size Label Length = $ThisSizeLength"
            
            # Choose the Largest of labels
            If ($ThisNameLength.CompareTo($ThisSizeLength) -ge 0){
                Write-Debug "Name Label is Larger"
                $MinDecimalPercent = $ThisNameLength / $Width
            }
            Else {
                Write-Debug "Size Label is Larger"
                $MinDecimalPercent = $ThisSizeLength / $Width
            }

            $DecimalPercent = $_.Size / $ThisDiskSize
            
            $Percent = If ($DecimalPercent -ge $MinDecimalPercent){
                Write-Debug "Using Actual"
                $DecimalPercent
            }
            Else{
                Write-Debug "Using Calculated Minimum"
                $MinDecimalPercent
            }

            [PSCustomObject]@{
                Name = $ThisName
                Size = $ThisSize
                Percent = $Percent #If ($DecimalPercent -ge $MinDecimalPercent){$DecimalPercent}Else{$MinDecimalPercent}
            }
        }

        
        #Draw
        $ThisPart = 1
        $PartCount = ([Array]$PartSizes).Count

        Write-Debug "PartCount = $PartCount"
        Write-Debug "PartNames = $($PartSizes.Name -join ',')"    
        Write-Debug "PartSizes = $($PartSizes.Percent -join ',')"

        Write-Host " Disk: $($_.DiskNo)" -ForegroundColor Yellow
        Write-Host " $('-'*$Width)"
        $PartSizes | %{
            If ($ThisPart -ne $PartCount) {
                # Any Partitions are started with a | and drawn to scale
                $Line1.Append("|".PadLeft($_.Percent*$Width)) | Out-Null
                #+2 here accounts for on preceeding ' ' and one trailing '|'
                #$Line2.Append(($($_.Name) + $(' '*($_.Percent*$Width - ($_.Name.Length + 2))) + "|").PadRight($_.Percent*$Width)) | Out-Null
                #$Line3.Append(($($_.Size) + $(' '*($_.Percent*$Width - ($_.Name.Length + 2))) + "|").PadRight($_.Percent*$Width)) | Out-Null
                $Line2.Append(" $($_.Name.PadRight($_.Percent*$Width - 3)) |") | Out-Null
                $Line3.Append(" $($_.Size.PadRight($_.Percent*$Width - 3)) |") | Out-Null
                $Line4.Append("|".PadLeft($_.Percent*$Width)) | Out-Null
            }
            Else {
                # On the Last partition we use all the remaining width and finish with a |
                $Line1.Append('|'.PadLeft($Remaining*$Width + 1)).ToString()
                # +1 here accounts for the preceedin ' ', the second +1 accounts forth last '|'... ithink
                #$Line2.Append(($($_.Name) + $(' '*($Remaining*$Width - ($_.Name.Length + 1))) + "|").PadRight($Remaining*$Width + 1 )).ToString()
                #$Line3.Append(($($_.Size) + $(' '*($Remaining*$Width - ($_.Name.Length + 2))) + "|").PadRight($Remaining*$Width + 1 )).ToString()
                $Line2.Append(" $($_.Name.PadRight($Remaining*$Width - 2)) |").ToString()
                $Line3.Append(" $($_.Size.PadRight($Remaining*$Width - 2)) |").ToString()
                $Line4.Append('|'.PadLeft($Remaining*$Width + 1)).ToString()

            }
            #We keep track of the remaining space left to be able to plot the last extended part
            $ThisPart++
            $Remaining = $Remaining - $_.Percent
        }
    
        Write-Host " $('-'*$Width)"
    }
}

$DiskConfig = @(
        [PSCustomObject]@{
            DiskNo = 0
            DiskSize = 1000
            Partitions = @(
                [PSCustomObject]@{
                    Name = 'System'
                    Size = 10
                },
                [PSCustomObject]@{
                    Name = 'System'
                    Size = 20
                },
                                [PSCustomObject]@{
                    Name = 'InsanelyLongPartitionNameToMakeThingsHard'
                    Size = 20
                },
                                [PSCustomObject]@{
                    Name = 'System'
                    Size = 20
                },
                [PSCustomObject]@{
                    Name = 'Windows'
                    Size = 0
                }
            )
        },
        [PSCustomObject]@{
            DiskNo = 1
            DiskSize = 1000
            Partitions = @(
                [PSCustomObject]@{
                    Name = 'Data'
                    Size = 0
                }
            )
        }
    )

cls
#Show-DiskLayout -CustomLayout $DiskConfig -Width 150 -v
Show-DiskLayout -Width 160 -v

