namespace VarsityMetrics;

public partial class EmailConfirmation : ContentPage
{
	public static string Email;
	public static string Password;
	public static string FirstName;
	public static string LastName;
    public static Constants.Role Role;

    public EmailConfirmation()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        int err = 0;
        tokenError.IsVisible = false;

        if (Token.Text == null | String.Equals(Token.Text, ""))
        {
            tokenError.Text = "Please fill in";
            tokenError.IsVisible = true;
            err = 1;
        }
        else if (Token.Text.Length < 6)
        {
            tokenError.Text = "Code must be at least 6 characters";
            tokenError.IsVisible = true;
            err = 1;
        }

        if(err == 0)
        {
            bool Confirmation = await App.db.ConfirmEmail(Email, Token.Text, Password, FirstName, LastName, Role);
		    if(Confirmation)
		    {
                App.CurrentUserAccount = new()
                {
                    Email = Email,
                    FirstName = FirstName,
                    LastName = LastName,
                    Role = Role
                };
                App.Current.MainPage = new AppShell();
            }
            else
            {
                tokenError.Text = "Incorrect code";
                tokenError.IsVisible = true;
            }
        }
		
    }

    private async void cancel_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new SignUpPage();
    }
}