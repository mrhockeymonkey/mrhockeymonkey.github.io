$Body = @"

"@

#Define Head 
$Head = @"
<title>Title</title>
<style> 
$(Get-Content -Path "$PSScriptRoot\style.css")
</style> 
"@

#Create the Html
$Html = (ConvertTo-Html -Body $Body -Head $Head | Out-String) -replace '<table>\r?\n</table>',''

#Send-MailMessage Needs a Credential so we just fake it
$Cred = New-Object System.Management.Automation.PSCredential 'UserName',('Password' | ConvertTo-SecureString -AsPlainText -Force)

#Send the Message
$Params = @{
    BodyAsHtml = $true
    Body       = $html 
    SmtpServer = '' 
    To         = '',''
    Cc         = '','',''
    From       = ''
    Subject    = '' 
    Credential = $Cred
}
Send-MailMessage @Params
