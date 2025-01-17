using VarsityMetrics.ViewModel;

namespace VarsityMetrics;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
    }
    private void LoginClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new AppShell();
    }

    private async void SignUpClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new SignUpPage();
    }

}