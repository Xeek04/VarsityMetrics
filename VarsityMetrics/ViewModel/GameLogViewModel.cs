using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace VarsityMetrics.ViewModels;

using System.Diagnostics.CodeAnalysis;
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
            // TODO: Perform additional logic, like navigation or updating details
        }
    }
}

