using System.Diagnostics;
using VarsityMetrics.DB_Models;

namespace VarsityMetrics;

public partial class StatsIndividual : ContentPage
{
    public Roster Player { get; set; }
    public PlayerStats PlayerStats { get; set; }

    public StatsIndividual(Roster player)
    {
        InitializeComponent();
        Player = player;
        BindingContext = this;
        Trace.WriteLine($"Player is {Player.Fname}");
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var query = App.db.StatQuery(Player.Fname, Player.Lname);
        PlayerStats = await query;
    }
}