namespace VarsityMetrics;

public partial class AccountPage : ContentPage
{
    public static String Username;
    public AccountPage()
	{
		InitializeComponent();
        username.Text = Username;
        Application.Current.UserAppTheme = AppTheme.Dark;
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

    private void Config(object sender, EventArgs e)
    {
            school.IsReadOnly = !school.IsReadOnly;
        division.IsReadOnly = !division.IsReadOnly;
        city.IsReadOnly = !city.IsReadOnly;

    }
}