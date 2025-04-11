using VarsityMetrics.DB_Models;

namespace VarsityMetrics;

public partial class MyTeam : ContentPage
{
	public MyTeam()
	{
        InitializeComponent();
        if (CurrentUser.Role == Constants.Role.Player)
        {
            teamrole.IsVisible = false;
            teamname.IsVisible = false;
            Addbutton.IsVisible = false;
        }
        if (CurrentUser.Role == Constants.Role.Recruiter)
        {
            teamrole.IsVisible = false;
            teamname.IsVisible = false;
            Addbutton.IsVisible = false;
        }
    }

    protected override async void OnAppearing()
	{
		base.OnAppearing();
		MyTeamList.ItemsSource = await App.db.RequestTeammateAsync();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        //if (account != null)
        //{
            bool uploadPlay = await App.db.UploadTeammateAsync(teamname.Text, teamrole.Text);
            MyTeamList.ItemsSource = await App.db.RequestTeammateAsync();
        //}
        //else
        //{

        //}
    }
}