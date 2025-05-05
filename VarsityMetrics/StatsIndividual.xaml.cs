using System.Diagnostics;
using VarsityMetrics.DB_Models;

namespace VarsityMetrics;

[QueryProperty(nameof(PlayerId), Constants.StatsIndividualNavKey)]
public partial class StatsIndividual : ContentPage
{
    public string PlayerId { get; set; }
    public Roster player;

    public StatsIndividual()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Load player data from DB using PlayerId
        if (int.TryParse(PlayerId, out int id))
        {
            List<Roster> playerList = await App.db.GetRoster();
            player = playerList.Where(p => p.Id.ToString() == PlayerId).FirstOrDefault();
            // Use `player` as needed
        }
    }
}