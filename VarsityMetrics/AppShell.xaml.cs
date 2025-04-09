using VarsityMetrics.DB_Models;

namespace VarsityMetrics
{
    public partial class AppShell : Shell

    {
        public bool IsCoachTabVisible { get; set; } = true;
        public AppShell()
        {
            InitializeComponent();

            BindingContext = this;

            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

            if (CurrentUser.Role == Constants.Role.Coach)
            {
                IsCoachTabVisible = false;
            }
        }
    }

}
