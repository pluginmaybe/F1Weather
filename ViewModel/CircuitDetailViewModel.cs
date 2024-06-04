namespace F1Weather.ViewModel;

[QueryProperty("Circuits", "Circuits")]
public partial class CircuitDetailViewModel : BaseViewModel
{
    readonly GetWeather _getWeather;
    [ObservableProperty]
    Color _weatherBackground;
    [ObservableProperty]
    Color _bckGroundColour;
    [ObservableProperty]
    bool _showWeather;
    [ObservableProperty]
    bool _showForecast;
    [ObservableProperty]
    string? _description;
    [ObservableProperty]
    string? _temp;
    [ObservableProperty]
    string? _windSpeed;
    [ObservableProperty]
    string? _main;
    [ObservableProperty]
    List<Forecast>? _forecasts;

    public CircuitDetailViewModel(GetWeather gw)
    {
        _getWeather = gw;
     
        _showWeather = false;
        _showForecast = false;
        _bckGroundColour = Color.FromRgb(0, 0, 0);
    }
    [ObservableProperty]
    Circuits? _circuits;

    [RelayCommand]
    void Back()
    {
        ShowForecast = false;
        ShowWeather = false;
        Shell.Current.Navigation.PopAsync();
        BckGroundColour = Color.FromRgb(0, 0, 0);
    }

    // Switch display based on if current weather or forecasted
    [RelayCommand]
    public async Task GetWeatherAsync()
    {
        if (ShowWeather)
        {
            ShowWeather = false;
            return;
        }
        if (ShowForecast)
        {
            ShowForecast = false;
        }
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
        Main = $"Current Weather : {cwm.Weather.First().Main.ToUpper()}";
        Description = $"({cwm.Weather.First().Description})";

        int temp = HelperMethods.ConvertKelvinToCelsius(cwm.Main.Temp);
        Temp = $"Temperature : {temp}c";
        SetBackGroundColour(temp);

        double wdspd = cwm.Wind.Speed;
        WindSpeed = $"Wind : {wdspd} meter/sec";

        

        ShowWeather = true;
    }
    [RelayCommand]
    public async Task GetForecastAsync()
    {
        if (ShowForecast)
        {
            ShowForecast = false;
            return;
        }
        if (ShowWeather)
        {
            ShowWeather = false;
        }
        // Todo understand data returned then 
        // devise best way to display on screen 
        // list of,
        // date / time / temp / desc / chance of rain / etc
        Forecasts = [];
        if (Circuits == null)
        {
            return;
        }
        _getWeather.SetDetails(Circuits.Latitude, Circuits.Longitude, false);
        ForeCastWeatherModel fcwm = await _getWeather.GetForecastFromApiAsync();
        foreach (var forecast in fcwm.List)
        {

            if (forecast == null || forecast.Weather is null || forecast.Main is null)
            {
                await Shell.Current.DisplayAlert("Error", "Error fetching Weather data", "OK");
                return;
            }
            Forecast stringForecast = new();
            DateTimeOffset convertedDt = HelperMethods.ConvertDTtoDateTime(forecast.Dt);

            stringForecast.DateAndTime = $"{convertedDt.DayOfWeek} : {convertedDt.TimeOfDay}";

            stringForecast.RainProbability = $"Rain : {forecast.Pop * 100}%";
            stringForecast.Description = $"{forecast.Weather.First().Description.ToUpper()}";
            int temp = HelperMethods.ConvertKelvinToCelsius(forecast.Main.Temp);
            stringForecast.Temperature = $"Temperature : {temp} c";


            Forecasts.Add(stringForecast);
        }
        ShowForecast = true;
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
        WeatherBackground = Color.FromRgb(r, g, b);
    }
}
