using System.Runtime.CompilerServices;

namespace VarsityMetrics;
public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

    }

    private void LightDark (object sender, EventArgs e)
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

    private void ColorChange (object sender, EventArgs e)
    {

    }

    private void TeamDecor (object sender, EventArgs e)
    {
        string thematic = "Ruston High";
        if (thematic == "Ruston High")
        {
            thematic = "Saints";
        }
        else if (thematic == "Saints")
        {
            thematic = "LSU";
        }
        else if(thematic == "LSU")
        {
            thematic = "Ruston High";
        }
    }
}