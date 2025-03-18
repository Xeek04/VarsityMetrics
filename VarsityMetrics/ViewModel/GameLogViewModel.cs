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
    private bool viewGrid;

    [ObservableProperty]
    private bool teamMode;

    [ObservableProperty]
    private bool boxMode;

    [ObservableProperty]
    private bool filmMode;

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

        // default mode is TeamMode
        TeamMode = true;

        // set this to true to see the main grid
        ViewGrid = true;
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
            if (Games.Count > 0) { 
                SelectedGame = Games.Last();
            }
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

    // when the tabs are clicked
    partial void OnTeamModeChanged(bool value)
    {
        if (value)
        {
            BoxMode = false;
            FilmMode = false;
        }
    }

    partial void OnBoxModeChanged(bool value)
    {
        if (value)
        {
            TeamMode = false;
            FilmMode = false;
        }
    }

    partial void OnFilmModeChanged(bool value)
    {
        if (value)
        {
            TeamMode = false;
            BoxMode = false;
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

    //tab button commands
    [RelayCommand]
    private void SetTeamMode()
    {
        TeamMode = true;
    }
    [RelayCommand]
    private void SetBoxMode()
    {
        BoxMode = true;
    }
    [RelayCommand]
    private void SetFilmMode()
    {
        FilmMode = true;
    }
}

