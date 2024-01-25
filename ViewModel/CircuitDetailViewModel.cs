using System.Xml;

namespace F1Weather.ViewModel;

[QueryProperty("Circuits", "Circuits")]
public partial class CircuitDetailViewModel : BaseViewModel
{
    readonly GetWeather _getWeather;
    [ObservableProperty]
    Color _bckGroundColour;
    [ObservableProperty]
    bool _updatedWeather;
    [ObservableProperty]
    string? _description;
    [ObservableProperty]
    string? _temp;
    [ObservableProperty]
    string? _windSpeed;
    public CircuitDetailViewModel(GetWeather gw)
    {
        _getWeather = gw;
        _updatedWeather = false;
        _bckGroundColour = Color.FromRgb(0,0,0);
    }
    [ObservableProperty]
    Circuits? _circuits;

    [RelayCommand]
    void Back()
    {
        UpdatedWeather = false;
        Shell.Current.Navigation.PopAsync();
        BckGroundColour = Color.FromRgb(0, 0, 0);
    }

    [RelayCommand]
    public async Task GetWeatherAsync()
    {
        if (Circuits == null)
        {
            return;
        }
        _getWeather.SetDetails(Circuits.Latitude, Circuits.Longitude, true);
        CurrentWeatherModel cwm = await _getWeather.GetWeatherFromApiAsync();
        if (cwm == null || cwm.Weather is null || cwm.Main is null || cwm.Wind is null)
        {
            await Shell.Current.DisplayAlert("Error", "Error fetching Weather data", "OK");
            return;
        }

        Description = $"Current Weather : {cwm.Weather.First().Description.ToUpper()}";
        
        int temp = HelperMethods.ConvertKelvinToCelsius(cwm.Main.Temp);
        Temp = $"Temperature : {temp}c";
        SetBackGroundColour(temp);
        
        double wdspd = cwm.Wind.Speed;
        WindSpeed = $"Wind : {wdspd}";

        UpdatedWeather = true;
    }
    [RelayCommand]
    public async Task GetForecastAsync()
    {
        if (Circuits == null)
        {
            return;
        }
        _getWeather.SetDetails(Circuits.Latitude, Circuits.Longitude, false);
        ForeCastWeatherModel fcwm = await _getWeather.GetForecastFromApiAsync();
        foreach (var forecast in fcwm.List)
        {

            if (forecast == null || forecast.Weather is null || forecast.Main is null || forecast.Wind is null)
            {
                await Shell.Current.DisplayAlert("Error", "Error fetching Weather data", "OK");
                return;
            }
            string p = forecast.Pop.ToString();
            Description = $"Current Weather : {forecast.Weather.First().Description.ToUpper()}";

            int temp = HelperMethods.ConvertKelvinToCelsius(forecast.Main.Temp);
            Temp = $"Temperature : {temp}c";
            SetBackGroundColour(temp);

            double wdspd = forecast.Wind.Speed;
            WindSpeed = $"Wind : {wdspd}";
        }
        UpdatedWeather = true;
    }

    void SetBackGroundColour(double temp)
    {
        int r;
        int g;
        int b;
        switch (temp)
        {
            case < 10:
                r = 40;
                g = 120;
                b = 170;
                break;
            case < 20:
                r = 110;
                g = 180;
                b = 40;
                break;
            case < 30:
                r = 160;
                g = 100;
                b = 20;
                break;
            default:
                r = 180;
                g = 40;
                b = 20;
                break;
        }
        BckGroundColour = Color.FromRgb(r, g, b);
    }
}
