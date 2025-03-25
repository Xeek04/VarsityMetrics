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
        await Task.Delay(500);

        await _viewModel.GetVideoCommand.ExecuteAsync(null);
        Trace.WriteLine($"GameLog.xaml.cs: Last game: {_viewModel.Games.Last().Opponent}");
        HistoryCollection.ScrollTo(_viewModel.Games.Last(), position: ScrollToPosition.Start, animate: false);
    }
}
