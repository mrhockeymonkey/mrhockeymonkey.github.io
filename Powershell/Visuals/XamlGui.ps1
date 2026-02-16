<#
	.SYNOPSIS
	Powershell script to render Xaml into a giu. 

	.NOTES
	Not a great working example. Works well when invoked from ISE but you may have to start
	powershell in STA mode to get this to work. Also may be dependancies on .NET Framework 
#>

#Setup a shared space for object to live between runspaces
$syncHash = [hashtable]::Synchronized(@{})

#Define a new runspace that will used for running powershell commands
$newRunspace =[runspacefactory]::CreateRunspace()
$newRunspace.ApartmentState = "STA"
$newRunspace.ThreadOptions = "ReuseThread"          
$newRunspace.Open()
$newRunspace.SessionStateProxy.SetVariable("syncHash",$syncHash)   

#Your initial XAML
[xml]$xaml = @" 
<Window
xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
Title="MainWindow" Height="350" Width="525">
    <Grid>
        <Image Name="i1" HorizontalAlignment="Left" Height="180" Margin="35,28,0,0" VerticalAlignment="Top" Width="354"/>
        <Button Name="b1" Content="Button" HorizontalAlignment="Left" Margin="419,28,0,0" VerticalAlignment="Top" Width="75"/>
        <Button Name="b2" Content="Button" HorizontalAlignment="Left" Margin="419,53,0,0" VerticalAlignment="Top" Width="75"/>
        <TextBlock Name="tb1" HorizontalAlignment="Left" Margin="35,230,0,0" TextWrapping="Wrap" Text="TextBlock" VerticalAlignment="Top" Height="81" Width="354"/>
    </Grid>
</Window> 
"@ 

$reader = New-Object System.Xml.XmlNodeReader $xaml 
$SyncHash.Window = [Windows.Markup.XamlReader]::Load($reader) 

#Define the elements on the Window
$SyncHash.i1 = $SyncHash.Window.FindName("i1")
$SyncHash.b1 = $SyncHash.Window.FindName("b1")
$SyncHash.b2 = $SyncHash.Window.FindName("b2")
$SyncHash.tb1 = $SyncHash.Window.FindName("tb1")

#Change some elements of the UI
$syncHash.i1.Source = $PSScriptRoot + "\ff.jpg"
$syncHash.tb1.Text = ""
$syncHash.b1.Content = "Services"
$syncHash.b2.Content = "Processes"


#Button Actions
$SyncHash.b1.add_click({
    #Define a new instance of powershell along with a script block to execute
    $psCmd = [PowerShell]::Create().AddScript({ 
        #From within this seperate thread you can change on screenelements via the sync hash table
        $syncHash.tb1.Dispatcher.Invoke([action]{$syncHash.tb1.text = "Getting Services..."},"Normal")
        $Services = (Get-Service).Name
        $syncHash.tb1.Dispatcher.Invoke([action]{$syncHash.tb1.AddText("`n$Services")},"Normal")
    })
    
    #Invoke the new powershell instance in a new runspace
    $psCmd.Runspace = $newRunspace
    $data = $psCmd.BeginInvoke() 
})

$SyncHash.b2.add_click({
    $psCmd = [PowerShell]::Create().AddScript({ 
        $syncHash.tb1.Dispatcher.Invoke([action]{$syncHash.tb1.text = "Getting Processes..."},"Normal")
        $Services = (Get-Process).Name
        $syncHash.tb1.Dispatcher.Invoke([action]{$syncHash.tb1.AddText("`n$Services")},"Normal")
    })
    
    $psCmd.Runspace = $newRunspace
    $data = $psCmd.BeginInvoke() 
})


#Show the Dialog
$SyncHash.Window.ShowDialog() | out-null






