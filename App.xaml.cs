using MetroLog.Maui;

namespace F1Weather;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
