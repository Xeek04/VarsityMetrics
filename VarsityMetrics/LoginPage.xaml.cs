using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Microsoft.Maui.ApplicationModel.Communication;
using VarsityMetrics.ViewModel;


namespace VarsityMetrics;

public partial class LoginPage : ContentPage
{
    public LoginPage()
	{
		InitializeComponent();
        BindingContext = this;
    }
    private async void LoginClicked(object sender, EventArgs e)
    {

        int err = 0;
        passwordError.IsVisible = false;
        emailError.IsVisible = false;

        if (password.Text == null | String.Equals(password.Text, ""))
        {
            passwordError.Text = "Please fill in";
            passwordError.IsVisible = true;
            err = 1;
        }
        if (email.Text == null | String.Equals(email.Text, ""))
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
            try
            {
                Constants.Role? role = await App.db.CheckLoginAsync(email.Text, password.Text);
                Trace.WriteLine($"LoginPage: Role is {role} (type: {role.GetType()})");

                if (role != null) //TODO make sure this works
                {
                    App.Current.MainPage = new AppShell();
                }
                else
                {
                    loginError.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Login Error", ex.Message, "OK");
            }
        }
    }
    

    private void SignUpClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new SignUpPage();
    }

    private void ForgotPasswordClicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new ForgotPassword();
    }


}