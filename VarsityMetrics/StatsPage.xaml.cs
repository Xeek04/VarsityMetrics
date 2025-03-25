using VarsityMetrics.DB_Models;

namespace VarsityMetrics;

public partial class StatsPage : ContentPage
{
    public StatsPage()
    {
        InitializeComponent();
        GetRoster();
    }

    private async void GetRoster()
    {
        playerList.ItemsSource = await App.db.GetPlayerList();
        playerListStats.ItemsSource = await App.db.GetPlayerList();
    }

    private void ShowStatEntries(object sender, EventArgs e)
    {
        Picker playerSelection = (Picker)sender;
        string player = playerSelection.SelectedItem.ToString();
        string[] parsedPlayer = player.Replace(",", null).Split();

        switch (parsedPlayer[3])
        {
            case "QB":
                PassingStats.IsVisible = true;
                RushingStats.IsVisible = true;
                ReceivingStats.IsVisible = false;
                break;

            case "RB":
                PassingStats.IsVisible = false;
                RushingStats.IsVisible = true;
                ReceivingStats.IsVisible = true;
                break;

            case "WR":
            case "TE":
                PassingStats.IsVisible = false;
                RushingStats.IsVisible = false;
                ReceivingStats.IsVisible = true;
                break;

            default:
                PassingStats.IsVisible = false;
                RushingStats.IsVisible = false;
                ReceivingStats.IsVisible = false;
                break;
        }
    }

    private void ShowStats(object sender, EventArgs e)
    {
        passbutton.IsVisible = false;
        rushbutton.IsVisible = false;
        recbutton.IsVisible = false;

        addplayerstats.IsVisible = !addplayerstats.IsVisible;
        playerListStats.IsVisible = !playerListStats.IsVisible;

        PassingStats.IsVisible = !PassingStats.IsVisible;
        RushingStats.IsVisible = !RushingStats.IsVisible;
        ReceivingStats.IsVisible = !ReceivingStats.IsVisible;

        atts.IsReadOnly = !atts.IsReadOnly;
        comps.IsReadOnly = !comps.IsReadOnly;
        passyds.IsReadOnly = !passyds.IsReadOnly;
        passtds.IsReadOnly = !passtds.IsReadOnly;
        ints.IsReadOnly = !ints.IsReadOnly;

        rushatt.IsReadOnly = !rushatt.IsReadOnly;
        rushyds.IsReadOnly = !rushyds.IsReadOnly;
        rushtds.IsReadOnly = !rushtds.IsReadOnly;
        fum.IsReadOnly = !fum.IsReadOnly;

        tar.IsReadOnly = !tar.IsReadOnly;
        rec.IsReadOnly = !rec.IsReadOnly;
        recyrds.IsReadOnly = !recyrds.IsReadOnly;
        rectds.IsReadOnly = !rectds.IsReadOnly;
    }

    private void AddStats(object sender, EventArgs e)
    {
        statnav.IsVisible = true;
        playerList.IsVisible = true;
        passbutton.IsVisible = true;
        rushbutton.IsVisible = true;
        recbutton.IsVisible = true;
        showplayerstats.IsVisible = false;
    }

    private void BackButton(object sender, EventArgs e)
    {
        HideAll();
    }

    private async void ConfirmAdd(object sender, EventArgs e)
    {
        PlayerStats stats = new PlayerStats{ Fname = "Joe", Lname = "Burrow", Position = "QB", PassAtt = 2 };
        await App.db.AddPlayerStats(stats);
        HideAll();
    }

    private void HideAll()
    {
        statnav.IsVisible = false;
        playerList.IsVisible = false;
        PassingStats.IsVisible = false;
        RushingStats.IsVisible = false;
        ReceivingStats.IsVisible = false;
        passbutton.IsVisible = false;
        rushbutton.IsVisible = false;
        recbutton.IsVisible = false;
        showplayerstats.IsVisible = true;
        ClearLines();
    }

    private void EntryToggle(object sender, EventArgs e)
    {
        Button toggle = (Button)sender;
        switch (toggle.CommandParameter)
        {
            case "pass":
                PassingStats.IsVisible = !PassingStats.IsVisible;
                break;
            case "rush":
                RushingStats.IsVisible = !RushingStats.IsVisible;
                break;
            case "rec":
                ReceivingStats.IsVisible = !ReceivingStats.IsVisible;
                break;
            default:
                break;
        }
    }

    private void ClearLines()
    {
        atts.Text = string.Empty;
        comps.Text = string.Empty;
        passyds.Text = string.Empty;
        passtds.Text = string.Empty;
        ints.Text = string.Empty;

        rushatt.Text = string.Empty;
        rushyds.Text = string.Empty;
        rushtds.Text = string.Empty;
        fum.Text = string.Empty;

        tar.Text = string.Empty;
        rec.Text = string.Empty;
        recyrds.Text = string.Empty;
        rectds.Text = string.Empty;
    }
}