namespace F1Weather.ViewModel;
public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel() { }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool _isBusy;
    [ObservableProperty]
    string _title = string.Empty;

    public bool IsNotBusy => !IsBusy;
}
