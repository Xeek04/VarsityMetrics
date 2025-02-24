using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace VarsityMetrics.ViewModels;

using System.Diagnostics.CodeAnalysis;
using System.Web;
using VarsityMetrics.DB_Models;

public partial class GameLogViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Game> games = new();

    [ObservableProperty]
    private Game? selectedGame;
    public IAsyncRelayCommand LoadGamesCommand { get; }

    [ObservableProperty]
    private bool isBusy = false;

    [ObservableProperty]
    private HtmlWebViewSource ytSource = new HtmlWebViewSource { Html = $@"
    <html>
    <body style='margin:0;padding:0;'>
        <iframe width='100%' height='100%' 
                src='https://www.youtube.com/embed/KoGKdLR-kc0?autoplay=1&controls=1'
                frameborder='0' allowfullscreen>
        </iframe>
    </body>
    </html>" };

    public GameLogViewModel()
    {
        LoadGamesCommand = new AsyncRelayCommand(LoadGames);
    }

    private async Task LoadGames()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            var games = await App.db.GetGamesAsync();
            Games.Clear();
            foreach (var game in games)
            {
                Games.Add(game);
            }
            Trace.WriteLine("GameLogViewModel: Games loaded successfully");
        }
        catch (Exception ex)
        {
            Trace.WriteLine($"GameLogViewModel: Error loading games - {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    // when the game is changed
    partial void OnSelectedGameChanged(Game? value)
    {
        //OnPropertyChanged(nameof(BannerText)); may be necessary????
        if (value != null)
        {
            Trace.WriteLine($"Selected game: {value.Opponent} - {value.ScoreDisplay}");
            //ytSource = ;
        }
    }

    private async Task<HtmlWebViewSource> getHtmlSourceByGameIdAsync(int gameId)
    {
        Footage footage = await App.db.getFootageByGameId(gameId);
        return new HtmlWebViewSource { Html = $@"
    <html>
    <body style='margin:0;padding:0;'>
        <iframe width='100%' height='100%' 
                src='https://www.youtube.com/embed/{footage.YtId}?autoplay=1&controls=1'
                frameborder='0' allowfullscreen>
        </iframe>
    </body>
    </html>" };
    }
}

