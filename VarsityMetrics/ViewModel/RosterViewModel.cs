using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VarsityMetrics;

using VarsityMetrics.DB_Models;

namespace VarsityMetrics.ViewModels;


public partial class RosterViewModel : ObservableObject
{

    public IAsyncRelayCommand OpenStatsCommand { get; }

    [ObservableProperty]
    private bool isBusy = false;

    public RosterViewModel()
    {
        OpenStatsCommand = new AsyncRelayCommand<object?>(OpenStats);
    }

    private async Task OpenStats(object parameter)
    {
        if (IsBusy) return;
        if (parameter == null)
        {
            Trace.WriteLine($"RosterViewModel: Command parameter null");
            return;
        }

        try
        {
            IsBusy = true;
            Roster player = parameter as Roster;
            Trace.WriteLine($"RosterViewModel: Command parameter is {player.Fname}");

            await Application.Current.MainPage.Navigation.PushAsync(new StatsIndividual(player));
        }
        catch (Exception ex)
        {
            Trace.WriteLine($"RosterViewModel: Error opening stats - {ex.Message}");
        }
        finally
        {
            Trace.WriteLine($"RosterViewModel: OpenStats complete");
            IsBusy = false;
        }

    }

}
