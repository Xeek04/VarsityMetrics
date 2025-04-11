using System.Diagnostics;
using VarsityMetrics.ViewModels;
using VarsityMetrics.DB_Models;

namespace VarsityMetrics;

public partial class GameLogPage : ContentPage
{
    private readonly GameLogViewModel _viewModel;

    public GameLogPage()
    {
        InitializeComponent();
        _viewModel = new GameLogViewModel();
        BindingContext = _viewModel;

        _viewModel.PauseVideoRequested += () =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (GameVideoPlayer != null)
                {
                    GameVideoPlayer.Pause();
                }
            });
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadGamesCommand.ExecuteAsync(null);
        await Task.Delay(500);

        await _viewModel.GetVideoCommand.ExecuteAsync(null);
        //Trace.WriteLine($"GameLog.xaml.cs: Last game: {_viewModel.Games.Last().Opponent}");
        //HistoryCollection.ScrollTo(_viewModel.Games.Last(), position: ScrollToPosition.Start, animate: false);
    }

    private async void EditSchedule(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SchedulePage));
    }

    private void HistoryCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void GameChanged(object sender, SelectionChangedEventArgs e)
    {
        int i = 0;
        bool check = true;
        while(check)
        {
            if (_viewModel.CurrentGame == null) { check = false; }
            else if (_viewModel.ScheduleStats[i].GameId == _viewModel.CurrentGame.GameId)
            {
                _viewModel.CurrentStats = _viewModel.ScheduleStats[i];
                check = false;
            }
            i++;
        }
    }
}
