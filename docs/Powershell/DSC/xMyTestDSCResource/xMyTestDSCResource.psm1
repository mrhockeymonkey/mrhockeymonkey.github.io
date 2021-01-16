Enum Ensure {
    Present
    Absent
}

[DscResource()]
Class xRandomTest {
    [DscProperty(Key)]
    [String]$Name

    [DscProperty(Mandatory)]
    [Ensure]$Ensure

    [DscProperty()]
    [String]$Optional

    [DscProperty(NotConfigurable)]
    [String]$ReadOnly

    [xRandomTest] Get() {
        $this.ReadOnly = "I`'m a Read Only Property"
        
        Return $this
    }

    [void] Set() {

    }

    [bool] Test() {
        Return $false
    }
}