using VarsityMetrics.DB_Models;

namespace VarsityMetrics;

public partial class Roster : ContentPage
{
    public Roster()
    {
        InitializeComponent();
    }

    private async void addPlayer(object sender, EventArgs e)
    {
        Button test = (Button)sender;
        await App.db.AddPlayer(Fname.Text, Lname.Text, test.CommandParameter.ToString());
        List<Player> something = await App.db.GetRoster();
        
        //test11.Text = something[0].Position;
    }

    private async void clearRoster(object sender, EventArgs e)
    {
        await App.db.ClearRoster();
        var something = await App.db.GetRoster();
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