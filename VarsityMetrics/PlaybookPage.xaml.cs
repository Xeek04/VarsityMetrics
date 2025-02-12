using Microsoft.UI.Xaml;

namespace VarsityMetrics;

public partial class PlaybookPage : ContentPage
{
	public PlaybookPage()
	{
		InitializeComponent();
	}


    private async void Button_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new AddPlaybook());
    }
}