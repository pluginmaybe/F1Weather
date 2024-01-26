namespace F1Weather.Models;

public class Forecast
{
    public string DateAndTime { get; set; } = string.Empty;
    public string RainProbability { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Temperature { get; set; } = string.Empty;
}
