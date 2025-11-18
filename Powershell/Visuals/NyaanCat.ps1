
    [CmdletBinding()]
    param(
        [int]$StrobeTime = 300
    )

$tstar = @"
+      o     +              o   
    +             o     +       +
o          +
    o  +           +        +
+        o     o       +        o
"@

$cat = @"
-_-_-_-_-_-_-_,------,      o 
_-_-_-_-_-_-_-|   /\_/\  
-_-_-_-_-_-_-~|__( ^ .^)  +     +  
_-_-_-_-_-_-_-""  ""      
"@

$cat1 = @"
-_-_-_-_-_-_-_,------,    
"@

$cat2 = @"
_-_-_-_-_-_-_-|   
"@

$cat3 = @"
-_-_-_-_-_-_-~|__
"@

$cat4 = @"
_-_-_-_-_-_-_-""  ""      
"@


$bstar = @"
+      o         o   +       o
    +         +
o        o         o      o     +
    o           +
+      +     o        o      +    
"@

    $c = 'blue','green','cyan','red','magenta','yellow'
    $pal = 'White','White','White','White'
    
    for ($i=0; $i -lt 1){ 
        $pal[3] = $pal[2]
        $pal[2] = $pal[1]
        $pal[1] = $pal[0]
        $pal[0] = $c[(Get-Random -Minimum 1 -Maximum 6)].tostring()
    
        Clear-Host
        Write-Host $tstar -ForegroundColor White
        Write-Host $cat1 -ForegroundColor $pal[0] -NoNewline
        Write-Host "  o "
        Write-Host $cat2 -ForegroundColor $pal[1] -NoNewline
        Write-Host "/\_/\"
        Write-Host $cat3 -ForegroundColor $pal[2] -NoNewline
        Write-Host "( ^ .^)  +     +"
        Write-Host $cat4 -ForegroundColor $pal[3]
        Write-Host $bstar -ForegroundColor White
        Start-Sleep -Milliseconds $StrobeTime
    }
