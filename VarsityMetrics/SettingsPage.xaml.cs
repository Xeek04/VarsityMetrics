using System.Runtime.CompilerServices;

namespace VarsityMetrics;
public partial class SettingsPage : ContentPage
{

    string thematic = "Ruston High";
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
    private void ColorChange(object sender, EventArgs e)
    {

    }
    private void TeamDecor (object sender, EventArgs e)
    {
        if (thematic == "Ruston High")
        {
            thematic = "Saints";
            // source = ImageSource.FromFile("dotnet_bot.png");
        }
        else if (thematic == "Saints")
        {
            thematic = "LSU";
            //Source = ImageSource.FromResource("dotnet_bot.png");
        }
        else if(thematic == "LSU")
        {
            thematic = "Ruston High";
            //Source = ImageSource.FromFile("dotnet_bot.png");
        }
        whatever.Text = thematic; 
    }
}