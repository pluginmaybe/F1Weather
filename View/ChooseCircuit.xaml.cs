namespace F1Weather.View;

public partial class ChooseCircuit : ContentPage
{
    
    public ChooseCircuit(CircuitViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}