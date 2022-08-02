# TODO

Quick little snippet I use for debugging modules�. If you run Get-Module <Module> and pipe to get-member, it contains a Invoke method.

Calling this method it will invoke a script block against the Modules scope.

This gets a bit funky, when you use $host.EnterNestedPrompt(). 

Run the below on an imported module, and you'll enter the running scope of that module. 

PS > & (Get-Module MachineDB) { $host.EnterNestedPrompt() }

This allows you to access private functions and variables. 

```
Add-Type -AssemblyName System.Windows.Forms

$Label = New-Object -TypeName System.Windows.Forms.Label
$Label.Text = "This is a Sample Form"
$Label.AutoSize = $true
$Label.BackColor = [System.Drawing.Color]::Transparent

$Font = New-Object System.Drawing.Font("Time New Roman",18,[System.Drawing.FontStyle]::Bold)

$Image = [System.Drawing.Image]::FromFile("C:\Users\Scott\Desktop\EmmaStone.jpg")

$Button = New-Object -TypeName System.Windows.Forms.Button
$Button.Location = New-Object -TypeName System.Drawing.Size(70,70)
$Button.Size = New-Object -TypeName System.Drawing.Size(75,23)

$Button.Name = "Button1"
$Button.Text = "Fit"

$Form = New-Object -TypeName System.Windows.Forms.Form
$Form.Text = "Sample Form"
#$Form.AutoSize = $true
#$Form.AutoSizeMode = [System.Windows.Forms.AutoSizeMode]::GrowOnly
$Form.MinimizeBox = $false
$Form.MaximizeBox = $false
$Form.ShowInTaskbar = $true
$form.StartPosition = [System.Windows.Forms.FormStartPosition]::CenterScreen
#$Form.BackColor = [System.Drawing.Color]::FromARGB(51,102,255)

$Form.Controls.Add($Label)
$Form.Controls.Add($Button)
$Form.Font = $Font
$Form.BackgroundImage = $Image
$Form.BackgroundImageLayout = [System.Windows.Forms.ImageLayout]::None
$Form.Width = $Image.Width
$Form.Height = $Image.Height
$Form.ShowDialog() | Out-Null
```