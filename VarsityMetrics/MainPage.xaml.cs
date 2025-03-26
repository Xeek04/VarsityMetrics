namespace VarsityMetrics;

public partial class MainPage : ContentPage
{
    public static String Username;
        public MainPage()
        {
            InitializeComponent();
            username.Text = Username;
        }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SchedulePage());
    }
}
