<#
	.SYNOPSIS
	A simple up/down arrow menu allowing user to select from options

	.NOTES
	Needs some cleaning up but the jist is there
#>

class Menu {
    [String]$Title
    [ConsoleColor]$TitleColor
    [String[]]$Items
    [ConsoleColor]$ItemColor
    [Int]$Selected = 1
    
    Menu() {
        $this.Title = 'Title Goes Here'
        $this.TitleColor = 'Yellow'
        $this.Items = 'Item1','Item2','Item3'
    }
    
    Draw() {
        #Create a Hash table of the Menu Items and assign each a unique Key
        $i = 1
        $ht = [System.Collections.Specialized.OrderedDictionary]@{}
        $this.Items | ForEach-Object {
            $ht.Add($_,$i)
            $i++
        }
        
        #Draw the menu
        Write-Host $this.Title -ForegroundColor $this.TitleColor
        $ht.Keys | ForEach-Object {
            if($ht.Item($_) -eq $this.Selected){
                Write-Host $_ -BackgroundColor Green
            }
            else{
                Write-Host $_
            }
        }
    }
}



$i = 1
$Page1 = [Menu]::New()
While($i -eq 1){

    $Page1.Draw()

    $KeyPress = [Console]::ReadKey("IncludeKeyDown")
    $KeyPress
    #Key UP
    if ($KeyPress.Key -eq 'UpArrow'){
        if($Page1.Selected -eq 1){
            $Page1.Selected = $Page1.Items.Count
        }
        else{
            $Page1.Selected--            
        }
        $Page1.Selected
    }
    #Key Down
    elseif($KeyPress.Key -eq 'DownArrow'){
        if($Page1.Selected -eq $Page1.Items.Count){
            $Page1.Selected = 1
        }
        else{
            $Page1.Selected++
        }
        $Page1.Selected
    }
    else{
        
    }
}





