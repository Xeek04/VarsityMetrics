using System.Diagnostics;
using VarsityMetrics.DB_Models;

namespace VarsityMetrics;

public partial class StatsIndividual : ContentPage
{
    public Roster Player { get; set; }

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
    }
}