namespace VarsityMetrics;

public partial class SchedulePage : ContentPage
{
	public SchedulePage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}