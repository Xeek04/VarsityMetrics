namespace VarsityMetrics;

public partial class AddPlaybook : ContentPage
{
		public static string filePath;
	public AddPlaybook()
	{
		InitializeComponent();
	}

    private async void Upload_Clicked(object sender, EventArgs e)
    {
		var result = await FilePicker.PickAsync(new PickOptions
		{
			FileTypes = FilePickerFileType.Images
		});

		if(result != null)
		{
			filePath = result.FullPath;
			apple.IsVisible = true;
			apple.Source = filePath;
		}
		
    }

    private void cancel_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }

    private async void confirm_Clicked(object sender, EventArgs e)
    {
		bool uploadPlay = await App.db.UploadPictureAsync(filePath, name.Text);
		if (uploadPlay)
		{
			Navigation.PopAsync();
		}
    }
}