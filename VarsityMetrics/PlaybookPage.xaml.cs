namespace VarsityMetrics;

public partial class PlaybookPage : ContentPage
{
	public PlaybookPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		PlaysList.ItemsSource = await App.db.RequestPictureAsync();
	}


	private void Button_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new AddPlaybook());
    }
}