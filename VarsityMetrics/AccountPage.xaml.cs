using VarsityMetrics.DB_Models;
namespace VarsityMetrics;

public partial class AccountPage : ContentPage
{
    public AccountPage()
	{
		InitializeComponent();
        username.Text = CurrentUser.Username;
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

    private void ConfigButton(object sender, EventArgs e)
    {
            school.IsReadOnly = !school.IsReadOnly;
        division.IsReadOnly = !division.IsReadOnly;
        city.IsReadOnly = !city.IsReadOnly;

    }
}