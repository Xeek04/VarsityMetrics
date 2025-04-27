using System.Text.RegularExpressions;
using VarsityMetrics.ViewModel;

namespace VarsityMetrics;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
    }

    private async void SignInClicked(object sender, EventArgs e)
    {

        int err = 0;
        firstError.IsVisible = false;
        lastError.IsVisible = false;
        passwordError.IsVisible = false;
        emailError.IsVisible = false;

        if(FirstName.Text == null | String.Equals(FirstName.Text, ""))
        {
            firstError.Text = "Please fill in";
            firstError.IsVisible = true;
            err = 1;
        }

        if (LastName.Text == null | String.Equals(LastName.Text, ""))
        {
            lastError.Text = "Please fill in";
            lastError.IsVisible = true;
            err = 1;
        }

        if (password.Text == null | String.Equals(password.Text, ""))
        {
            passwordError.Text = "Please fill in";
            passwordError.IsVisible = true;
            err = 1;
        }
        else if(password.Text.Length < 6)
        {
            passwordError.Text = "Password must be at least 6 characters";
            passwordError.IsVisible = true;
            err = 1;
        }

        if(email.Text == null | String.Equals(email.Text,""))
        {
            emailError.Text = "Please fill in";
            emailError.IsVisible = true;
            err = 1;
        }
        else if (!Regex.IsMatch(email.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
        {
            emailError.Text = "Email is not in the proper format";
            emailError.IsVisible = true;
            err = 1;
        }

        if (err == 0)
        {
            bool createAccount = await App.db.InsertAccountAsync(password.Text, email.Text);
            if (createAccount)
            {
                
                EmailConfirmation.Email = email.Text;
                EmailConfirmation.Password = password.Text;
                EmailConfirmation.FirstName = FirstName.Text;
                EmailConfirmation.LastName = LastName.Text;

                App.Current.MainPage = new EmailConfirmation();
            }
            else
            {
                accountError.IsVisible = true;
            }
        }
    }

    private void LoginClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new LoginPage();
    }
}