using VarsityMetrics.DB_Models;

namespace VarsityMetrics
{
    public partial class AppShell : Shell

    {
        public AppShell()
        {
            InitializeComponent();

            BindingContext = this;

            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

            if (CurrentUser.Role == Constants.Role.Recruiter)
            {
                PlaybookPageTab.IsVisible = false;
            }
            
        }
    }

}
