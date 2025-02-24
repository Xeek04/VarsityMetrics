using System.Diagnostics;
using VarsityMetrics.ViewModels;

namespace VarsityMetrics;

public partial class GameLogPage : ContentPage
{
    private readonly GameLogViewModel _viewModel;

    public GameLogPage()
    {
        InitializeComponent();
        _viewModel = new GameLogViewModel();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadGamesCommand.ExecuteAsync(null);
    }
}
