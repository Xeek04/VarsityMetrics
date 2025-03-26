using Microsoft.Extensions.Logging;
using VarsityMetrics.DB_Models;
using CommunityToolkit.Maui;

namespace VarsityMetrics
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<DBAccess>(s => ActivatorUtilities.CreateInstance<DBAccess>(s, Constants.DatabasePath));

            return builder.Build();
        }
    }
}
