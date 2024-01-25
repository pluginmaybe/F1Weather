namespace F1Weather.Models;
public class Circuits
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Region {  get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime StartTime { get; set; }
    public string CircuitImagePath { get; set; } = string.Empty;
}
