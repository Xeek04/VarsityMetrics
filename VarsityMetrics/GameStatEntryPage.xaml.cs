using VarsityMetrics.ViewModels;
using VarsityMetrics.DB_Models;
namespace VarsityMetrics;

public partial class GameStatEntryPage : ContentPage
{
	public GameStatEntryPage()
	{
        InitializeComponent();
        PopulateGames();
	}

    private void Back(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void PopulateGames()
    {
        List<Gamelog> gamelog = await App.db.GetSchedule();
        List<string> result = new List<string>();
        foreach (Gamelog game in gamelog)
        {
            if (!game.StatsEntered) { result.Add(game.OppTeam); }
        }
        
        games.ItemsSource = result;
    }
}