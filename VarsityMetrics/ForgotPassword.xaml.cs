namespace VarsityMetrics;

public partial class ForgotPassword : ContentPage
{
	public ForgotPassword()
	{
		InitializeComponent();
	}

    private async void Send_Clicked(object sender, EventArgs e)
    {
		//bool Sent = await App.db.ForgotPasswordEmail(email.Text);
    }
}