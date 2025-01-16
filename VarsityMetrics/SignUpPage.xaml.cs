using VarsityMetrics.ViewModel;

namespace VarsityMetrics;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignInViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}