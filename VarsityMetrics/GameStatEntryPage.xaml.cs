using VarsityMetrics.ViewModels;
using VarsityMetrics.DB_Models;
using System.Runtime.CompilerServices;
using VarsityMetrics.ViewModel;
namespace VarsityMetrics;


/* visibility of quarter ___sections___:
 * Start off invisible
 * When a game is picked, ItemChanged clears the grids, then calls PopulatePlays which makes the sections visible based on if there are any plays
 * EnterPlay calls CheckQ which makes the entered play's section visible if it isn't
 * 
 * Conclusion: Sections should always be visible as long as the selected game isn't null and the child grid has rows
 * Should update when picker changes and when new lines are added
*/

public partial class GameStatEntryPage : ContentPage
{
    private readonly GameStatEntryViewModel _viewModel;

    private Dictionary<string, int> gameKey = new Dictionary<string, int>();
    private Dictionary<string, int> playKey = new Dictionary<string, int>();
  	private Dictionary<int, Grid> quarterKey = new Dictionary<int, Grid>();
    private Dictionary<int?, string> downKey = new Dictionary<int?, string>();
    
    public GameStatEntryPage()
	{

        InitializeComponent();

        _viewModel = new GameStatEntryViewModel();
        BindingContext = _viewModel;

	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.PopulatePickersCommand.ExecuteAsync(null);
    }


    // leave page
    private void Back(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }


}