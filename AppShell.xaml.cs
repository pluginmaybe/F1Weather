namespace F1Weather;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ChooseCircuit), typeof(ChooseCircuit));
        Routing.RegisterRoute(nameof(CircuitPage), typeof(CircuitPage));
    }
}
