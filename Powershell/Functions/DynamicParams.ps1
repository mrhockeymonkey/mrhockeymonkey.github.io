<#
	.SYNOPSIS
	An example of using dynamic parameters

	.DESCRIPTION
	When designing a function for common use or use by less advanced users dynamic variables can
	be extremely helpful in making sure they enter sensible params by offering a limited set of choices
#>

Function Do-Something {
    [CmdletBinding()]
    Param (
        $Path
    )
    DynamicParam {
        #DynamicParam block expects this as output
        $DynamicParams = New-Object System.Management.Automation.RuntimeDefinedParameterDictionary
        
        $Param1 = New-Object System.Management.Automation.RuntimeDefinedParameter
        
        #Define the Param Name
        $Param1.Name = 'ChildItem'
        
        #Define the Param type
        $Param1.ParameterType = [System.String]
        
        #Define the Param Attributes
        $Param1Attr = New-Object System.Management.Automation.ParameterAttribute
        $Param1Attr.Mandatory = $true
        $Param1Attr.Position = 0
        $Param1Attr.HelpMessage = 'I am a dynamic param'
        $Param1.Attributes.Add($Param1Attr)

        #Validation
        $Param1Validation = New-Object System.Management.Automation.ValidateSetAttribute -ArgumentList ((Get-ChildItem -Path $Path).Name)
        $Param1.Attributes.Add($Param1Validation)

        #Now Add your params to the Param dictionary and return
        $DynamicParams.Add('ChildItem', $Param1)
        Return $DynamicParams
    }
    PROCESS {
        
        #Dydnamic parameter wnt be intatiated as a variable you you need to use PSBoundParameters
        $PSBoundParameters.ContainsKey('ChildItem')

        Write-Host "Dynamically Selected: $($PSBoundParameters['ChildItem'])"
        #Write-Host "You selected: $ChildItem"
    }
}