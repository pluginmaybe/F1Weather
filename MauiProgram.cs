using MetroLog.MicrosoftExtensions;
using OfficeOpenXml;

namespace F1Weather;
public static class MauiProgram
{
    
    public static MauiApp CreateMauiApp()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<GetWeather>();
        builder.Services.AddSingleton<CircuitService>();
        builder.Services.AddSingleton<ChooseCircuit>();
        builder.Services.AddSingleton<CircuitPage>();

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<CircuitDetailViewModel>();
        builder.Services.AddSingleton<CircuitViewModel>();
        builder.Logging.AddTraceLogger(options =>{
            options.MinLevel = LogLevel.Debug;
        });
        //builder.Logging.AddStreamingFileLogger(_ => { });

        return builder.Build();
    }
}
