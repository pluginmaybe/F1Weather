namespace F1Weather.View;

public partial class CircuitPage : ContentPage
{
	public CircuitPage(CircuitDetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}