namespace BlazorWasmSpa.Data;

public class PlantEntity
{
    public int PlantId { get; set; }
    public string PlantName { get; set; }
    public double PlantHeightCm { get; set; }
    public DateTimeOffset LastWatered { get; set; }
}