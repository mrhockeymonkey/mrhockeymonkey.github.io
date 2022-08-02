# Using

```powershell
# one way to overcome double hop
$Cred = Get-Credential
Invoke-Command -ComputerName vipwds1 -Credential $cred -ScriptBlock {
    Invoke-Command -ComputerName gspwds1 -Credential $using:Cred -ScriptBlock {
        $env:Computername
    }
}
```