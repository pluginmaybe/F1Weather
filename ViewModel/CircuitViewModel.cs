namespace F1Weather.ViewModel;
public partial class CircuitViewModel : BaseViewModel
{
    readonly ICircuitService _circuitService;
    readonly ILogger _logger;

    [ObservableProperty]
    Color _bckGroundColour;

    public ObservableCollection<Circuits> Circuits { get; } = [];
    public CircuitViewModel(ILogger<CircuitViewModel> logger, ICircuitService circuitService)
    {
        _circuitService = circuitService;
        _logger = logger;
        _bckGroundColour = new Color(120, 120, 0);
        GetCircuitsAsync().Wait();
    }
    [ObservableProperty]
    bool _isRefreshing = false;

    [RelayCommand]
    static void BackToMain()
    {
        Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task GoToDetailsAsync(Circuits circuit)
    {
        if (circuit is null)
        {
            _logger.LogError("Circuit is null");
            return;
        }
        _logger.LogInformation("Go to details page");
        await Shell.Current.GoToAsync($"{nameof(CircuitPage)}", true,
            new Dictionary<string, object>
            {
                { "Circuits", circuit }
            });
    }

    [RelayCommand]
    async Task GetCircuitsAsync()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            _logger.LogInformation("Attempting to retrieve Circuit data");
            var circuits = await _circuitService.GetCircuits();
            //
            if (Circuits.Count != 0)
            {
                Circuits.Clear();
            }
            foreach (var c in circuits)
            {
                Circuits.Add(c);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failure To retrieve Circuit data");

        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
            _logger.LogInformation("Refreshing Complete");
        }
    }
}
