namespace VarsityMetrics;

public partial class ResetPassword : ContentPage
{
    public static string Email;
    public ResetPassword()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        int err = 0;
        passwordError.IsVisible = false;

        if (password.Text == null | String.Equals(password.Text, ""))
        {
            passwordError.Text = "Please fill in";
            passwordError.IsVisible = true;
            err = 1;
        }
        else if (password.Text.Length < 6)
        {
            passwordError.Text = "Password must be at least 6 characters";
            passwordError.IsVisible = true;
            err = 1;
        }

        if (err == 0)
        {
            bool Confirmation = await App.db.ResetPassword(password.Text);
            if (Confirmation)
            {
                App.Current.MainPage = new LoginPage();
            }
            else
            {
                passwordError.Text = "Incorrect code";
                passwordError.IsVisible = true;
            }
        }

    }

    private async void cancel_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new ForgotPassword();
    }
}