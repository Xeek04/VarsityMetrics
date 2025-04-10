using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace VarsityMetrics.ViewModels;

using System.Diagnostics.CodeAnalysis;
using System.Web;
using VarsityMetrics.DB_Models;
using static System.Net.WebRequestMethods;

public partial class GameLogViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Gamelog> games = new();

    [ObservableProperty]
    private Gamelog? selectedGame;
    public IAsyncRelayCommand LoadGamesCommand { get; }
    public IAsyncRelayCommand GetVideoCommand { get; }

    public IRelayCommand ChangeVideoCommand { get; }

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
    private bool notNullVideo;

    [ObservableProperty]
    private string? mediaSource;

    [ObservableProperty]
    private string changeVideoText;

    [ObservableProperty]
    private bool entryVisible = false;

    [ObservableProperty]
    private string newUri;  // Binds to the Entry text

    public event Action? PauseVideoRequested;


    public GameLogViewModel()
    {
        LoadGamesCommand = new AsyncRelayCommand(LoadGames);
        GetVideoCommand = new AsyncRelayCommand(GetVideo);
        ChangeVideoCommand = new AsyncRelayCommand(UpdateVideoUriAsync);

        // default mode is TeamMode
        TeamMode = true;

        // set this to true to see the main grid
        ViewGrid = false;
    }

    private async Task LoadGames()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            var games = await App.db.GetSchedule();
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
            Trace.WriteLine($"GameLogViewModel: LoadGames complete");
            IsBusy = false;
        }
    }

    private async Task GetVideo()
    {
        Trace.WriteLine($"GameLogViewModel: starting GetVideo");
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            Trace.WriteLine($"GameLogViewModel: selected game is {SelectedGame}");
            if (SelectedGame != null)
            {
                Trace.WriteLine($"GameLogViewModel: selected game not null");
                int selectedGamePk = SelectedGame.Pk;
                Footage film = await App.db.GetFootageByGameIdAsync(selectedGamePk);
                MediaSource = film?.Uri;
            } else
            {
                Trace.WriteLine($"GameLogViewModel: selected game null");
                NotNullVideo = false;
            }
        }
        catch (Exception ex)
        {
            Trace.WriteLine($"GameLogViewModel: Error loading footage - {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task UpdateVideoUriAsync()
    {
        if (!string.IsNullOrWhiteSpace(NewUri) && SelectedGame != null)
        {
            bool success = await App.db.UploadVideoAsync(SelectedGame.Pk, NewUri);
            if (success)
            {
                MediaSource = NewUri; // Update the MediaElement source
                Trace.WriteLine($"New Video URI Uploaded: {NewUri}");
                NewUri = "";
                EntryVisible = false;
            }
            else
            {
                Trace.WriteLine("Failed to upload video URI.");
            }
        }
    }

    // when the game is changed
    partial void OnSelectedGameChanged(Game? value)
    {
        Trace.WriteLine($"GameLogViewModel: selected game changed");
        //OnPropertyChanged(nameof(BannerText)); may be necessary????
        if (value != null)
        {
            GetVideoCommand.ExecuteAsync(null);
            Trace.WriteLine($"Selected game: {value.Opponent} - {value.ScoreDisplay}");
            //ytSource = ;

            //TODO load film when game is changed
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
        // else pause the mediaElement
        else
        {
            PauseVideoRequested?.Invoke(); // Notify the view to pause the video
            EntryVisible = false;
        }
    }

    partial void OnMediaSourceChanged(string? value)
    {
        if (value == null)
        {
            NotNullVideo = false;
            ChangeVideoText = "Upload video...";
        }
        else
        {
            NotNullVideo = true;
            ChangeVideoText = "Change video...";
        }
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

    [RelayCommand]
    private void OpenChangeVideo()
    {
        if (EntryVisible)
        {
            EntryVisible = false;
        }
        else
        {
            EntryVisible = true;
        }
    }
}

