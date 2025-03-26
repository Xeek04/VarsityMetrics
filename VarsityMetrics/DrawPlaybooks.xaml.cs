using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Maui.Views;

namespace VarsityMetrics;

public partial class DrawPlaybooks : ContentPage
{
	public DrawPlaybooks()
	{
		InitializeComponent();
	}

    private async void SaveDrawing_Clicked(object sender, EventArgs e)
    {

    }

    private void CancelDrawing_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}