namespace VarsityMetrics;

public partial class MainPage : ContentPage
{
    public static String Username;
        public MainPage()
        {
            InitializeComponent();
            
            username.Text = App.CurrentUserAccount != null ? "Welcome back, " + App.CurrentUserAccount.FirstName + "!" : "Welcome Back!";
        }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SchedulePage());
    }
}
