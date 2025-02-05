namespace VarsityMetrics;

public partial class AccountPage : ContentPage
{
	public static String Username;
	public AccountPage()
	{
		InitializeComponent();
		username.Text = Username;
	}
}