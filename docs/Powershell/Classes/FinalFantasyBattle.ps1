<#
    .SYNOPSIS
	Example classes using Final Fantasy 7 battles as a theme.
	
	.DESCRIPTION
	This script demonstrates some of the things you can do with powershell classes
    Using the FF7 Battle system as an example. 
#>

#Parent Class
class Battle {
    #Define a Constructor
    Battle() {
        $this.PartyMembers = 'Cloud','Tifa','Barret'
        $this.Terrain = Get-Random -Minimum 1 -Maximum 3
    }
    #Define Properties
    [Terrain]$Terrain
    [Int]$PartyCount = 3
    #Array from Enum
    [PartyMembers[]]$PartyMembers
    PartyMemberKO() {
        $this.PartyCount--
        if($this.PartyCount -eq 0){
            Write-Host "Game Over!" -ForegroundColor Red
        } 
    }
    Victory(){
        [Array]$Notes = $null
        $Notes += @{Freq = 580; Dur = 110} # C
        $Notes += @{Freq = 580; Dur = 110} # C
        $Notes += @{Freq = 580; Dur = 110} # C
        $Notes += @{Freq = 580; Dur = 380} # C
        $Notes += @{Freq = 460; Dur = 380} # G#
        $Notes += @{Freq = 505; Dur = 380} # Bb
        $Notes += @{Freq = 580; Dur = 210} # C
        $Notes += @{Freq = 505; Dur = 110} # Bb
        $Notes += @{Freq = 580; Dur = 460} # C
        
        
        $Notes | ForEach-Object {
            [Console]::Beep($_.Freq,$_.Dur)
        }
    }
}

#Inherits from [Battle]
class RandomBattle : Battle {
    RandomBattle() {
        $this.EnemyCount = Get-Random -Minimum 1 -Maximum 5
        for($i=1; $i -le $this.EnemyCount; $i++) {$this.Enemies += Get-Random -Minimum 1 -Maximum ([Enum]::GetNames([Enemies]).count + 1)}
    }
    [Int]$EnemyCount
    [Enemies[]]$Enemies 
}

class BossBattle : Battle {
    #Overloaded Constructor Method
    BossBattle($Terrain,$Enemy) {
        $this.Terrain = $Terrain
        $this.Enemy = $Enemy
    }
    #Overrides Battle Properties
    [Int]$EnemyCount = 1
    [String]$Enemy
    [String]$Terrain
}

enum PartyMembers {
    Cloud = 1
    Tifa = 2
    Barret = 3
    Aeris = 4
}

enum Enemies {
    Cactar = 1
    Tonberry = 2
    Malboro = 3
    Frog = 4
    Beheamoth = 5
    Adamantoise = 6
    Bomb = 7
}

enum Terrain {
    Midgar = 1
    Nibleheim = 2
    NorthernCrater = 3
}

Write-Host "Creating Random Battle" -ForegroundColor Yellow
$RandomBattle = [RandomBattle]::New()
$RandomBattle

Write-Host "Creating Boss Battle" -ForegroundColor Yellow
$BossBattle = [BossBattle]::New('TempleOfAncients','Sephiroth')
$BossBattle



