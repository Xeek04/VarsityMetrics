namespace VarsityMetrics;
using Supabase;
using VarsityMetrics.DB_Models; 


public partial class AccountPage : ContentPage
{
    public AccountPage()
	{
        
        InitializeComponent();
        //Application.Current.UserAppTheme = AppTheme.Dark;
        var currentUser = DBAccess.client.Auth.CurrentUser;

        if (currentUser != null)
        {
            string email = currentUser.Email;
            emailPres.Text = currentUser.Email;
            Console.WriteLine($"Current logged-in user's email is: {email}");
        }
        else
        {
            Console.WriteLine("No user is currently logged in.");
        }

    }
    private void LightDark(object sender, EventArgs e)
    {
        if (Application.Current.UserAppTheme == AppTheme.Dark)
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
    }

    private async void LogOut_Clicked(object sender, EventArgs e)
    {
        await App.db.LogOutAsync();

        App.Current.MainPage = new LoginPage();

    }
}