using System.Drawing;

namespace F1Weather.ViewModel;
public partial class MainViewModel : BaseViewModel
{
    public MainViewModel() { }

    [RelayCommand]
    async Task NavigateToChoose()
    {
        await Shell.Current.GoToAsync($"{nameof(ChooseCircuit)}");
    }


}
