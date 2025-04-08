namespace VarsityMetrics;

public partial class EmailConfirmation : ContentPage
{
	public static string Email;
	public static string Password;

	public EmailConfirmation()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		bool Confirmation = await App.db.ConfirmEmail(Email, Token.Text);
		if(Confirmation)
		{
            App.Current.MainPage = new AppShell();
        }
		
    }
}