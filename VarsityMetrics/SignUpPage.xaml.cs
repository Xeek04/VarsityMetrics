using VarsityMetrics.ViewModel;

namespace VarsityMetrics;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
    }

    private async void SignInClicked(object sender, EventArgs e)
    {

        int err = 0;
        passwordError.IsVisible = false;
        usernameError.IsVisible = false;
        emailError.IsVisible = false;

        if (password.Text == null | String.Equals(password.Text, ""))
        {
            passwordError.Text = "Please fill in";
            passwordError.IsVisible = true;
            err = 1;
        }
        if (username.Text == null | String.Equals(username.Text, ""))
        {
            usernameError.Text = "Pleas fill in";
            usernameError.IsVisible = true;
            err = 1;
        }
        if(email.Text == null | String.Equals(email.Text,""))
        {
            emailError.Text = "Please fill in";
            emailError.IsVisible = true;
            err = 1;
        }

        if (err == 0)
        {
            await App.db.InsertAccountAsync(username.Text, password.Text, email.Text);
            App.Current.MainPage = new AppShell();
        }
    }

    private void LoginClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new LoginPage();
    }
}