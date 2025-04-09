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
        string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string pictureNamePath = Path.Combine("Pictures", name.Text + ".jpg");
        string picturesPath = Path.Combine(userProfile, pictureNamePath);

        int selectedIndex = TypePicker.SelectedIndex;
        string type = (string)TypePicker.ItemsSource[selectedIndex];

        using var draw = await DrawView.GetImageStream(1024, 1024);
        using var memoryDraw = new MemoryStream();
        draw.CopyTo(memoryDraw);

        draw.Position = 0;
        memoryDraw.Position = 0;

#if WINDOWS
    await System.IO.File.WriteAllBytesAsync(picturesPath, memoryDraw.ToArray());

#endif
        bool uploadPlay = await App.db.UploadPictureAsync(picturesPath, name.Text, type);
        if(uploadPlay)
        {
            Navigation.PopAsync();
        }

    }

    private void CancelDrawing_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}