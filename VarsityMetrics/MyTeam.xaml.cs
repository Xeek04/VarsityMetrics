namespace VarsityMetrics;

public partial class MyTeam : ContentPage
{
	public MyTeam()
	{
        InitializeComponent();
    }

    protected override async void OnAppearing()
	{
		base.OnAppearing();
		MyTeamList.ItemsSource = await App.db.RequestTeammateAsync();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        bool uploadPlay = await App.db.UploadTeammateAsync(teamname.Text, teamrole.Text);
        MyTeamList.ItemsSource = await App.db.RequestTeammateAsync();
    }
}