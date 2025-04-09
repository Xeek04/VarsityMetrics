namespace VarsityMetrics;

public partial class EmailConfirmation : ContentPage
{
	public static string Email;
	public static string Password;
	public static string FirstName;
	public static string LastName;

	public EmailConfirmation()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		bool Confirmation = await App.db.ConfirmEmail(Email, Token.Text, Password, FirstName, LastName);
		if(Confirmation)
		{
            App.Current.MainPage = new AppShell();
        }
		
    }
}