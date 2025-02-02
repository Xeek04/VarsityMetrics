using VarsityMetrics.ViewModel;

namespace VarsityMetrics;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
    }
    private void LoginClicked(object sender, EventArgs e)
    {
        int err = 0;
        passwordError.IsVisible = false;
        usernameError.IsVisible = false;

        if (password.Text == null | String.Equals(password.Text, ""))
        {
            passwordError.Text = "Please fill in";
            passwordError.IsVisible = true;
            err = 1;
        }
        if(username.Text == null | String.Equals(username.Text, ""))
        {
            usernameError.Text = "Pleas fill in";
            usernameError.IsVisible = true;
            err = 1;
        }
        
        if(err == 0)
        {
            App.Current.MainPage = new AppShell();
        }
    }

    private async void SignUpClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new SignUpPage();
    }

}