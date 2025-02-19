using VarsityMetrics.DB_Models;

namespace VarsityMetrics;

public partial class Roster : ContentPage
{
    private Label input = new Label { Text = "testing" };
    private Dictionary<Button, Grid> gridKey = new Dictionary<Button, Grid>();
    
    public Roster()
	{
		InitializeComponent();
        gridKey.Add(QBButton, addQB);
	}
    private async void addPlayer(object sender, EventArgs e)
    {
        Button test = (Button)sender;
        //await App.db.AddPlayer(Fname.Text, Lname.Text, test.CommandParameter.ToString());
        List<Player> something = await App.db.GetRoster();

        //test11.Text = something[0].Position;
    }

    private async void clearRoster(object sender, EventArgs e)
    {
        await App.db.ClearRoster();
        var something = await App.db.GetRoster();
    }

    private void entryLine(object sender, EventArgs e)
    {
        Grid view = gridKey.GetValueOrDefault((Button)sender);
        view.Add(input);
    }

    private void openEditRoster(object sender, EventArgs e)
    {
        addQB.IsVisible = !addQB.IsVisible;
        addRB.IsVisible = !addRB.IsVisible;
        addWR.IsVisible = !addWR.IsVisible;
        addTE.IsVisible = !addTE.IsVisible;
        addOL.IsVisible = !addOL.IsVisible;
        addDE.IsVisible = !addDE.IsVisible;
        addDT.IsVisible = !addDT.IsVisible;
        addLB.IsVisible = !addLB.IsVisible;
        addCB.IsVisible = !addCB.IsVisible;
        addS.IsVisible = !addS.IsVisible;
    }
}