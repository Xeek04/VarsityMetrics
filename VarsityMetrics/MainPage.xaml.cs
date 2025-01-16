namespace VarsityMetrics
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
        private void onPBClick (object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new NewPage1());
        }
        private void onSQClick(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new NewPage2());
        }
        private void onGLClick(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new NewPage3());
        }
    }


}
