#Here is an example of sending a basic HTML email via powershell. You can optionally attch images inline which allows you to embed images. 

#First Create the message
$smtpServer = "SMTP.YourServer.com"
$msg = new-object Net.Mail.MailMessage
$smtp = new-object Net.Mail.SmtpClient($smtpServer)

#Then add various properties to the message
$msg.From = '<a href="mailto:From@YourServer.com">From@YourServer.com</a>'
$msg.ReplyTo = '<a href="mailto:From@YourServer.com">From@YourServer.com</a>'
$msg.To.Add('<a href="mailto:To@YourServer.com">To@YourServer.com</a>')
$msg.subject = "This is an email with inline images"
$msg.IsBodyHtml = $True

#Now Define the HTML body of the message
$body = @"
<html>
<body>
<img src="cid:image1.jpg">
<table>
<tr><td></td></tr>
</table>
</body>
</html>
"@

#Then Send the message
$smtp.Send($msg)
$attachment.Dispose();
$msg.Dispose();


#You can attach images inline with this method
$attachment = New-Object System.Net.Mail.Attachment –ArgumentList "C:\image1.jpg"
$attachment.ContentDisposition.Inline = $True
$attachment.ContentDisposition.DispositionType = "Inline"
$attachment.ContentType.MediaType = "image/jpg"
$attachment.ContentId = 'image1.jpg'
$msg.Attachments.Add($attachment)


#or multiple attachents
$path = C:\images
$files= Get-ChildItem $path
 
Foreach($file in $files)
{
 
$attachment = New-Object System.Net.Mail.Attachment –ArgumentList $Path\$file.ToString() #convert file-system object type to string
$attachment.ContentDisposition.Inline = $True
$attachment.ContentDisposition.DispositionType = "Inline"
$attachment.ContentType.MediaType = "image/jpg"
$attachment.ContentId = $file.ToString()
$msg.Attachments.Add($attachment)
 
}
