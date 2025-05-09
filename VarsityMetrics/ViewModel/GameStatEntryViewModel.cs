using VarsityMetrics.DB_Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using Microsoft.IdentityModel.Tokens;

namespace VarsityMetrics.ViewModel;

public partial class GameStatEntryViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isBusy = false;

    [ObservableProperty]
    private bool navMode;

    [ObservableProperty]
    private bool entryMode;

    [ObservableProperty]
    private bool q1SIsVisible;

    [ObservableProperty]
    private bool q2SIsVisible;

    [ObservableProperty]
    private bool q3SIsVisible;

    [ObservableProperty]
    private bool q4SIsVisible;

    [ObservableProperty]
    private bool startIsEnabled;

    [ObservableProperty]
    private List<Gamelog> games;

    [ObservableProperty]
    private Gamelog? selectedGame;

    [ObservableProperty]
    private List<Play> plays;

    [ObservableProperty]
    private Play? selectedPlay;

    [ObservableProperty]
    private List<int> downs;

    [ObservableProperty]
    private int? selectedDown;

    [ObservableProperty]
    private List<int> quarters;

    [ObservableProperty]
    private int? selectedQuarter;

    [ObservableProperty]
    private List<string> yardageTypes;

    [ObservableProperty]
    private string? selectedYardageType;

    [ObservableProperty]
    private List<PlayerStats> players;

    [ObservableProperty]
    private PlayerStats? selectedPlayer1;

    [ObservableProperty]
    private PlayerStats? selectedPlayer2;

    [ObservableProperty]
    private string? yards;

    [ObservableProperty]
    private string? time = "15:00";

    // Async commands
    public IAsyncRelayCommand PopulatePickersCommand { get; }
    public IAsyncRelayCommand EnterPlayCommand { get; }

    // Plays

    private ObservableCollection<PlayByPlay> _q1Plays = [];

    private ObservableCollection<PlayByPlay> _q2Plays = [];

    private ObservableCollection<PlayByPlay> _q3Plays = [];

    private ObservableCollection<PlayByPlay> _q4Plays = [];

    public ObservableCollection<PlayByPlay> Q1Plays
    {
        get => _q1Plays;
        set
        {
            if (_q1Plays != value)
            {
                _q1Plays = value;
                OnPropertyChanged(); // Important: this notifies the UI
                OnQ1PlaysChanged();  // Your custom logic, if needed
            }
        }
    }

    public ObservableCollection<PlayByPlay> Q2Plays
    {
        get => _q2Plays;
        set
        {
            if (_q2Plays != value)
            {
                _q2Plays = value;
                OnPropertyChanged(); // Important: this notifies the UI
                OnQ2PlaysChanged();  // Your custom logic, if needed
            }
        }
    }

    public ObservableCollection<PlayByPlay> Q3Plays
    {
        get => _q3Plays;
        set
        {
            if (_q3Plays != value)
            {
                _q3Plays = value;
                OnPropertyChanged(); // Important: this notifies the UI
                OnQ3PlaysChanged();  // Your custom logic, if needed
            }
        }
    }

    public ObservableCollection<PlayByPlay> Q4Plays
    {
        get => _q4Plays;
        set
        {
            if (_q4Plays != value)
            {
                _q4Plays = value;
                OnPropertyChanged(); // Important: this notifies the UI
                OnQ4PlaysChanged();  // Your custom logic, if needed
            }
        }
    }

    public GameStatEntryViewModel()
    {
        // make async relay commands
        PopulatePickersCommand = new AsyncRelayCommand(PopulatePickers);
        EnterPlayCommand = new AsyncRelayCommand(EnterPlay);

        // set up page
        NavMode = true;
        Time = "15:00";
        Q1SIsVisible = false;
        Q2SIsVisible = false;
        Q3SIsVisible = false;
        Q4SIsVisible = false;
        OnSelectedGameChanged(SelectedGame);

        // hooking events
        Q1Plays.CollectionChanged += (s, e) => OnQ1PlaysChanged();
        Q2Plays.CollectionChanged += (s, e) => OnQ2PlaysChanged();
        Q3Plays.CollectionChanged += (s, e) => OnQ3PlaysChanged();
        Q4Plays.CollectionChanged += (s, e) => OnQ4PlaysChanged();

        // Connects each grid to an int signifying its number

        // Connects each down number to english

    }

    // Async Relay Commands

    private async Task PopulatePickers()
    {
        if (IsBusy) return;

        // query db
        Task<List<Gamelog>> gamesTask = App.db.GetSchedule();
        Task<List<Play>> playsTask = App.db.GetPlays();
        Task<List<PlayerStats>> playerStatsTask = App.db.GetPlayerStatsAsync();

        // add unentered games to navbar
        List<Gamelog> allGames = await gamesTask;
        Games = allGames.Where(g => g.StatsEntered == false).ToList();

        // add plays to playbook
        List<Play> dbPlays = await playsTask;
        Plays = dbPlays.DistinctBy(p => p.name).ToList();

        //add players to player picker
        List<PlayerStats> playerStats = await playerStatsTask;
        Players = playerStats;

        // populate down picker
        Downs = [1, 2, 3, 4];

        // populate quarter picker
        Quarters = [1, 2, 3, 4];

        // populate yardage types
        YardageTypes = ["Pass", "Run"];
    }

    public async Task LoadPlaysAsync(int gameId)
    {
        Trace.WriteLine($"GameStatEntryViewModel: LoadPlaysAsync starting");
        Q1Plays.Clear();
        Q2Plays.Clear();
        Q3Plays.Clear();
        Q4Plays.Clear();

        var q1 = await App.db.GetPlayByPlay(gameId, 1);
        foreach (var p in q1) Q1Plays.Add(p);

        var q2 = await App.db.GetPlayByPlay(gameId, 2);
        foreach (var p in q2) Q2Plays.Add(p);

        var q3 = await App.db.GetPlayByPlay(gameId, 3);
        foreach (var p in q3) Q3Plays.Add(p);

        var q4 = await App.db.GetPlayByPlay(gameId, 4);
        foreach (var p in q4) Q4Plays.Add(p);
        Trace.WriteLine($"GameStatEntryViewModel: LoadPlaysAsync complete. Q1:{q1.Count()}, Q2:{q2.Count()}, Q3:{q3.Count()}, Q4:{q4.Count()}, ");
        Trace.WriteLine($"GameStatEntryViewModel: Totals: {Q1Plays.Count()}, {Q2Plays.Count()}, {Q3Plays.Count()}, {Q4Plays.Count()}");
    }

    private async Task LoadPlaysSafeAsync(int gameId)
    {
        try
        {
            Trace.WriteLine($"GameStatEntryViewModel: LoadPlaysSafeAsync starting");
            await LoadPlaysAsync(gameId); // Your actual logic
        }
        catch (Exception ex)
        {
            Trace.WriteLine($"GameStatEntryViewModel:Error loading plays: {ex.Message}");
            // Optionally notify user or log more
        }
    }

    private async Task EnterPlay()
    {
        // if any of them are null or empty then return
        if (SelectedGame is null || SelectedPlayer1 == null || SelectedQuarter is null ||
            SelectedYardageType is null || Yards is null || SelectedDown is null || string.IsNullOrWhiteSpace(Yards))
        {
            Trace.WriteLine($"GameStatEntryViewModel: One or more fields is null");
            return;
        }

        // if any of them are in the wrong format then return
        if (!Time.IsNullOrEmpty() && Time.Except(['1','2','3','4','5','6','7','8','9','0',':','-']).Any())
        {
            Trace.WriteLine($"GameStatEntryViewModel: Time is in improper format");
            return;
        }

        if (!int.TryParse(Yards, out _))
        {
            Trace.WriteLine($"GameStatEntryViewModel: Yards is in improper format");
            return;
        }

        // if there's no second player for a pass then return
        if (SelectedYardageType == "Pass" && SelectedPlayer2 == null)
        {
            Trace.WriteLine($"GameStatEntryViewModel: Must have a second player for pass play");
            return;
        }

        var play = new PlayByPlay
        {
            GameId = SelectedGame.GameId,
            Down = SelectedDown.Value,
            Quarter = SelectedQuarter.Value,
            GameTime = Time,
            YardType = SelectedYardageType,
            Yards = int.Parse(Yards),
            PlayId = SelectedPlay.play_id,
            Players = SelectedYardageType == "Pass"
                ? [ SelectedPlayer1.AbvName, SelectedPlayer2.AbvName ]
                : [ SelectedPlayer1.AbvName ]
        };

        switch (play.Quarter)
        {
            default:
                Q1Plays.Add(play);
                break;
            case 2:
                Q2Plays.Add(play);
                break;
            case 3:
                Q3Plays.Add(play);
                break;
            case 4:
                Q4Plays.Add(play);
                break;
        }


        await App.db.AddPlayToGame(play);
        await App.db.AddPlaybookStats(SelectedPlay.name, "Offense", int.Parse(Yards));

    }

    // Button Commands

    [RelayCommand]
    private void SetEntryMode()
    {
        EntryMode = true;
    }

    [RelayCommand]
    private void SetNavMode()
    {
        // TODO set game's StatsEntered value to True in the database, then update relevant UI stuff
        NavMode = true;
    }


    // OnChanged
    partial void OnEntryModeChanged(bool value)
    {
        if (value)
        {
            NavMode = false;
        }
    }

    partial void OnNavModeChanged(bool value)
    {
        if (value)
        {
            EntryMode = false;
        }
    }

    partial void OnSelectedGameChanged(Gamelog value)
    {
        // Disable start when no game selected
        if (value == null)
        {
            Trace.WriteLine($"GameStatEntryViewModel: Selected game is null");

            StartIsEnabled = false;

        }
        else
        {
            Trace.WriteLine($"GameStatEntryViewModel: Selected game is {value.OppTeam}");
            StartIsEnabled = true;
            _ = LoadPlaysSafeAsync(value.GameId);
        }
    }

    private void OnQ1PlaysChanged()
    {
        Q1SIsVisible = Q1Plays.Any();
        Trace.WriteLine($"GameStatEntryViewModel: Q1 {(Q1SIsVisible ? "Visible" : "Invisible")}");
    }

    private void OnQ2PlaysChanged()
    {
        Q2SIsVisible = Q2Plays.Any();
    }

    private void OnQ3PlaysChanged()
    {
        Q3SIsVisible = Q3Plays.Any();
    }

    private void OnQ4PlaysChanged()
    {
        Q4SIsVisible = Q4Plays.Any();
        Trace.WriteLine($"GameStatEntryViewModel: Q4 {(Q4SIsVisible ? "Visible" : "Invisible")}");
    }
}

public class GreaterThanZeroConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (value != null && (int)value > 0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class TimeEntryBehavior : Behavior<Entry>
{
    private static readonly char[] AllowedChars = "0123456789:-".ToCharArray();

    protected override void OnAttachedTo(Entry entry)
    {
        entry.TextChanged += OnTextChanged;
        base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.TextChanged -= OnTextChanged;
        base.OnDetachingFrom(entry);
    }

    private void OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            var filtered = new string(e.NewTextValue
                .Where(c => AllowedChars.Contains(c))
                .ToArray());

            if (entry.Text != filtered)
                entry.Text = filtered;
        }
    }
}

public class YardEntryBehavior : Behavior<Entry>
{
    private static readonly char[] AllowedChars = "0123456789".ToCharArray();

    protected override void OnAttachedTo(Entry entry)
    {
        entry.TextChanged += OnTextChanged;
        base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.TextChanged -= OnTextChanged;
        base.OnDetachingFrom(entry);
    }

    private void OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            var filtered = new string(e.NewTextValue
                .Where(c => AllowedChars.Contains(c))
                .ToArray());

            if (entry.Text != filtered)
                entry.Text = filtered;
        }
    }
}
