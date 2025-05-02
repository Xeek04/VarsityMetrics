namespace VarsityMetrics;

public partial class ForgotPassword : ContentPage
{
	public ForgotPassword()
	{
		InitializeComponent();
	}

    private async void Send_Clicked(object sender, EventArgs e)
    {
		bool Sent = await App.db.ForgotPasswordEmail(email.Text);

		if (Sent)
		{
            ResetPasswordToken.Email = email.Text;
            App.Current.MainPage = new ResetPasswordToken();
		}
    }

    private async void cancel_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new LoginPage();
    }
}