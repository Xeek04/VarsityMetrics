namespace VarsityMetrics
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

            GoToAsync("//LoginPage");
        }
    }
}