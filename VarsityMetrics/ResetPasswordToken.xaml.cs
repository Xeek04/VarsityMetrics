namespace VarsityMetrics;

public partial class ResetPasswordToken : ContentPage
{
    public static string Email;
    public ResetPasswordToken()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        int err = 0;
        tokenError.IsVisible = false;

        if (Token.Text == null | String.Equals(Token.Text, ""))
        {
            tokenError.Text = "Please fill in";
            tokenError.IsVisible = true;
            err = 1;
        }
        else if (Token.Text.Length < 6)
        {
            tokenError.Text = "Code must be at least 6 characters";
            tokenError.IsVisible = true;
            err = 1;
        }

        if (err == 0)
        {
            bool Confirmation = await App.db.ResetPasswordToken(Email, Token.Text);
            if (Confirmation)
            {
                ResetPassword.Email = Email;
                App.Current.MainPage = new ResetPassword();
            }
            else
            {
                tokenError.Text = "Incorrect code";
                tokenError.IsVisible = true;
            }
        }

    }

    private async void cancel_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new ForgotPassword();
    }
}