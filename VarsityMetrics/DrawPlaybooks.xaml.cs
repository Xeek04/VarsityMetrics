using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Platform;

namespace VarsityMetrics;

public partial class DrawPlaybooks : ContentPage
{
	public DrawPlaybooks()
	{
		InitializeComponent();
	}

    private async void SaveDrawing_Clicked(object sender, EventArgs e)
    {
        /*int selectedIndex = TypePicker.SelectedIndex;

        string type = (string)TypePicker.ItemsSource[selectedIndex];*/

        var draw = await DrawView.GetImageStream(800, 600);
        
    }

    private void CancelDrawing_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}