#Example Pie Chart Generation
[void][Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms.DataVisualization")

#Define the Frame
$MemoryUsageChart1 = New-object System.Windows.Forms.DataVisualization.Charting.Chart
$MemoryUsageChart1.Width = 800
$MemoryUsageChart1.Height = 400
$MemoryUsageChart1.BackColor = [System.Drawing.Color]::White
		 
#Define the Header 
$MemoryUsageChart1.Titles.Add("Physical Memory Usage: Top 5 Processes")
$MemoryUsageChart1.Titles[0].Font = "segoeuilight,20pt"
$MemoryUsageChart1.Titles[0].Alignment = "topLeft"

#Define and Add the Chart Area		 
$chartarea = New-Object System.Windows.Forms.DataVisualization.Charting.ChartArea
$chartarea.Name = "ChartArea1"
$3dStyle = New-Object System.Windows.Forms.DataVisualization.Charting.ChartArea3DStyle
$3dStyle.Enable3D = $true
$chartarea.Area3DStyle = $3dStyle
$MemoryUsageChart1.ChartAreas.Add($chartarea)

#Define and Add the Data
$MemoryUsageChart1.Series.Add("data1")
$MemoryUsageChart1.Series["data1"].ChartType = [System.Windows.Forms.DataVisualization.Charting.SeriesChartType]::Pie
$Processes = Get-Process | Sort-Object -Property WS | Select-Object Name,PM,VM -Last 5
$ProcessList = @(foreach($Proc in $Processes){$Proc.Name + "`n"+[math]::floor($Proc.PM/1MB)})
$Placeholder = @(foreach($Proc in $Processes){$Proc.PM})

#Plot the Graph
$MemoryUsageChart1.Series["data1"].Points.DataBindXY($ProcessList, $Placeholder)
$MemoryUsageChart1.SaveImage("$env:TEMP\Physical_Memory_Usage.png","png")

Invoke-Item "$env:TEMP\Physical_Memory_Usage.png"
