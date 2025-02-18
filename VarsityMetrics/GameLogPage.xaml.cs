using System.Diagnostics;

namespace VarsityMetrics;

public partial class GameLogPage : ContentPage
{
    public GameLogPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Trace.WriteLine("GameLogPage.xaml.cs: OnAppearing()");

        try
        {
            HistoryCollection.ItemsSource = await App.db.GetGamesAsync();
            Trace.WriteLine("GameLogPage.xaml.cs: GetGamesAsync() complete");
        }
        catch (Exception ex)
        {
            Trace.WriteLine($"GameLogPage.xaml.cs: Error fetching games - {ex.Message}");
        }
    }
    private void OnHistorySelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return; // Nothing selected

        // Get the selected game
        var selectedGame = e.CurrentSelection.FirstOrDefault() as DB_Models.Game;

        if (selectedGame != null)
        {
            // Debugging output
            Trace.WriteLine($"Selected game: {selectedGame.Opponent} - {selectedGame.ScoreDisplay}");

            // Perform any action (e.g., navigate to details page)
        }

    // Optional: Clear selection to allow re-selection of the same item
    ((CollectionView)sender).SelectedItem = null;
    }
}