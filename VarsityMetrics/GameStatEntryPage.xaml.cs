using VarsityMetrics.ViewModels;
using VarsityMetrics.DB_Models;
namespace VarsityMetrics;

public partial class GameStatEntryPage : ContentPage
{
	public GameStatEntryPage()
	{
        InitializeComponent();
	}

    private void Back(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}