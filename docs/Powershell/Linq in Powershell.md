# Linq in Powershell

Notes on article: https://www.red-gate.com/simple-talk/sysadmin/powershell/high-performance-powershell-linq/

Linq methods are static extension methods which, in powershell, are called as below

```powershell
[ClassName]::MethodName(ObjectInstance, arguments...)
```

Delegates in linq are written as

```powershell
# var result = dates.Where(d => d.Year > 2016);
[Func[DateTime,bool]] $delegate = { param($d); return $d.Year -gt 2016 }
[Linq.Enumerable]::Where($dates, $delegate)
```

## Count

```powershell
[int[]] $numbers = @(3, 1, 4, 1, 5, 9, 2)
[Linq.Enumerable]::Count($numbers) # 7
[Linq.Enumerable]::Count($numbers, [Func[int,bool]] { $args[0] -gt 2 }) # 4
```

## FirstOrDefault

```powershell
[int[]] $numbers = @(2, 0, 5, -11, 29)
[Linq.Enumerable]::FirstOrDefault($numbers) # 2

$delegate = [Func[int,bool]] { $args[0] -gt 100 }
[Linq.Enumerable]::First($numbers, $delegate) # 0
```

## Select

```powershell
[DateTime[]]$dates = 
    (Get-Date -Year 2017 -Month 10 -Day 23),
    (Get-Date -Year 2013 -Month 12 -Day 3),
    (Get-Date -Year 2016 -Month 2 -Day 13)

[Linq.Enumerable]::Select($dates, [Func[DateTime,int]] { $args[0].DayOfYear})
296
337
44
```