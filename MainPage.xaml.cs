namespace F1Weather;

public partial class MainPage : ContentPage
{
    readonly ILogger _logger;
    public MainPage(ILogger<MainPage> logger, MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _logger = logger;

        
        

        _logger.LogInformation("Main Page initialized");
    }   

    
}

