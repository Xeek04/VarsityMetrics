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
		int err = 0;
		NameError.IsVisible = false;
		TypeError.IsVisible = false;

        if (name.Text == null | String.Equals(name.Text, ""))
        {
            NameError.Text = "Please fill in";
            NameError.IsVisible = true;
            err = 1;
        }

        bool uploadPlay = false;
		int selectedIndex = TypePicker.SelectedIndex;

        if (selectedIndex == -1)
        {
			TypeError.Text = "Please select an option";
			TypeError.IsVisible = true;
			err = 1;
        }

		if(err == 0)
		{
			string type = (string)TypePicker.ItemsSource[selectedIndex];
			uploadPlay = await App.db.UploadPictureAsync(filePath, name.Text, type);

			if (uploadPlay)
			{
				Navigation.PopAsync();
			}
		}

    }
}