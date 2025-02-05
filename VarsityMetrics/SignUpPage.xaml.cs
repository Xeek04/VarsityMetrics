using VarsityMetrics.ViewModel;

namespace VarsityMetrics;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
    }

    private void SignInClicked(object sender, EventArgs e)
    {
        // TODO App.db.InsertAccountAsync("Sam", "Vandiver", "sjv008");
        App.Current.MainPage = new AppShell();
    }

    private void LoginClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new LoginPage();
    }
}