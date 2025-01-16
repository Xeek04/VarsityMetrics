using VarsityMetrics.ViewModel;

namespace VarsityMetrics;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}