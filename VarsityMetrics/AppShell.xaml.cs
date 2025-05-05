using Microsoft.Maui.ApplicationModel.Communication;
using VarsityMetrics.DB_Models;

namespace VarsityMetrics
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(EmailConfirmation), typeof(EmailConfirmation));
            Routing.RegisterRoute(nameof(ForgotPassword), typeof(ForgotPassword));


            Routing.RegisterRoute(nameof(SchedulePage), typeof(SchedulePage));
            Routing.RegisterRoute(nameof(StatsIndividual), typeof(StatsIndividual));
            Routing.RegisterRoute(nameof(GameStatEntryPage), typeof(GameStatEntryPage));

            _ = InitShellAsync();


        }
        private async Task InitShellAsync()
        {
            string? role = await App.db.GetCurrentUserRoleAsync();
            if (role == "Scout")
            {
                // PlayPage.IsVisible = false;
                // GameLPage.IsVisible = false;
                // MyTeamPage.IsVisible = false;
            }
        }
    }
}