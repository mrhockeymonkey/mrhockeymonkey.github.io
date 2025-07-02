function New-PieChart {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true)]
        [String[]]$Labels,

        [parameter(Mandatory=$true)]
        [Int[]]$Values,

        [parameter(Mandatory=$true)]
        [String]$Title,

        [parameter(Mandatory=$true)]
        [ValidatePattern('.*.png$')]
        [String]$Path,

        [Int]$Height = 400,

        [Int]$Width = 600
    )
    BEGIN{
        [void][Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms.DataVisualization")
    }
    PROCESS {
        #Define the Frame

        $Chart = New-object System.Windows.Forms.DataVisualization.Charting.Chart
        $Chart.Width = $Width
        $Chart.Height = $Height
        $Chart.BackColor = [System.Drawing.Color]::White
        		 
        #Define the Header 

        $Chart.Titles.Add($Title)
        $Chart.Titles[0].Font = "segoeuilight,20pt"
        $Chart.Titles[0].Alignment = "topLeft"
        
        #Define and Add the Chart Area
        		 
        $chartarea = New-Object System.Windows.Forms.DataVisualization.Charting.ChartArea
        $chartarea.Name = "ChartArea1"
        $3dStyle = New-Object System.Windows.Forms.DataVisualization.Charting.ChartArea3DStyle
        $3dStyle.Enable3D = $true
        $chartarea.Area3DStyle = $3dStyle
        $Chart.ChartAreas.Add($chartarea)
        
        #Add the Data and Plot the Graph
        
        $Chart.Series.Add("data1")
        $Chart.Series["data1"].ChartType = [System.Windows.Forms.DataVisualization.Charting.SeriesChartType]::Pie
        $Chart.Series["data1"].Points.DataBindXY($Labels, $Values)
        $Chart.SaveImage($Path,"png")
    }
    END {}
}


$Labels = @('Cloud','Tifa','Barret','Aeries')
$Values = @(100,50,50,20)

New-PieChart -Labels $Labels -Values $Values -Title 'Final Fantasy Pie' -Path 'E:\Pie.png'

E:\Pie.png