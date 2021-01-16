$ConfigurationData = @{

    AllNodes = @(
        @{
            NodeName = 'Node1'
        },
        @{
            NodeName = 'Node2'
        }
    )
}

Configuration MyTestLab {
    #Param (
    #    [Parameter()]
    #    [String]$Node = 'localhost',
    #
    #    [Parameter()]
    #    [String]$HyperVPath = 'C:\Hyper-V'
    #)

    Import-DscResource -ModuleName xHyper-V
        
    xVMSwitch LabSwitch {
        Name   = 'LabSwitch'
        Type   = 'Internal'
        Ensure = 'Present'
    }

    $AllNodes | ForEach-Object {
        xVHD $_.NodeName {
            Name             = $_.NodeName
            Path             = "C:\Hyper-V"
            Ensure           = 'Present'
            Generation       = 'Vhdx'
            MaximumSizeBytes = 30GB
        }

        xVMHyperV $_.NodeName {
            Name            = $_.NodeName
            VhdPath         = "C:\Hyper-V\$($_.NodeName).vhdx"
            Ensure          = 'Present'
            SwitchName      = 'LabSwitch'
            Generation      = 2
            SecureBoot      = $false
            MaximumMemory   = 2048MB
            State           = 'Running'
            RestartIfNeeded = $true
            DependsOn       = "[xVHD]$($_.NodeName)"
        }

    }
}

MyTestLab -OutputPath C:\tmp\dsc -ConfigurationData $ConfigurationData