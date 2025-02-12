namespace VarsityMetrics;

public partial class AddPlaybook : ContentPage
{
	public AddPlaybook()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		var result = await FilePicker.PickAsync(new PickOptions
		{
			FileTypes = FilePickerFileType.Images
		});

		if(result != null)
		{
			var filePath = result.FullPath;
			apple.IsVisible = true;
			button.IsVisible = false;
			apple.Source = filePath;
		}
		
    }
}