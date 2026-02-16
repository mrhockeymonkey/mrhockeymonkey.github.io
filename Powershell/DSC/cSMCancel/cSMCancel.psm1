Enum Action {
    Stop
    Continue
}

[DSCResource()]
Class Cancel {
    [DSCProperty(Key)]
    [Action]$Action

    [Cancel]Get() {
        Return $this
    }
    [Void]Set() {
        Write-Verbose "Forcibly Stopping DSC Configuration"
        Stop-DscConfiguration -Force -Verbose:$false
    }
    [Bool]Test() {
        $ShouldCancel = $this.ShouldCancel()
        Write-Verbose "Action: $($this.Action)"
        Write-Verbose "Should Cancel: $ShouldCancel"
        
        If ($ShouldCancel -and $this.Action -eq [Action]::Stop) {
            Return $false
        }
        Else {
            Return $true
        }
    }
    [Bool]ShouldCancel() {
        #Logic to determine if DSC should be cancelled or not
        Return (Test-Path C:\tmp\Cancel.txt)
    }
}