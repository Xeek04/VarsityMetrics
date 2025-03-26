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

    private void StatSelection(object sender, EventArgs e)
    {
        passbutton.IsVisible = false;
        rushbutton.IsVisible = false;
        recbutton.IsVisible = false;
        statnav.IsVisible = true;
        
        addplayerstats.IsVisible = !addplayerstats.IsVisible;
        playerListStats.IsVisible = !playerListStats.IsVisible;
    }

    private void ShowStats(object sender, EventArgs e)
    {
        string[] player = playerListStats.SelectedItem.ToString().Replace(",", null).Split();
        PlayerStats stats = new PlayerStats { Fname = player[1], Lname = player[0] };

        if (stats.PassAtt != null)
        {
            PassingStats.IsVisible = !PassingStats.IsVisible;        
            atts.IsReadOnly = !atts.IsReadOnly;
            comps.IsReadOnly = !comps.IsReadOnly;
            passyds.IsReadOnly = !passyds.IsReadOnly;
            passtds.IsReadOnly = !passtds.IsReadOnly;
            ints.IsReadOnly = !ints.IsReadOnly;

            atts.Text = stats.PassAtt.ToString();
            comps.Text = stats.PassComp.ToString();
            passyds.Text = stats.PassYards.ToString();
            passtds.Text = stats.PassTDs.ToString();
            ints.Text = stats.Interceptions.ToString();
        }
        if (stats.RushAtt != null)
        {
            RushingStats.IsVisible = !RushingStats.IsVisible;
            rushatt.IsReadOnly = !rushatt.IsReadOnly;
            rushyds.IsReadOnly = !rushyds.IsReadOnly;
            rushtds.IsReadOnly = !rushtds.IsReadOnly;
            fum.IsReadOnly = !fum.IsReadOnly;
        }

        ReceivingStats.IsVisible = !ReceivingStats.IsVisible;

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
        //PlayerStats stats = new PlayerStats{ Fname = "Joe", Lname = "Burrow", Position = "QB", PassAtt = 2 };
        string[] current = playerList.SelectedItem.ToString().Replace(",", null).Split();
        PlayerStats stats = new PlayerStats { Fname = current[1], Lname = current[0] };
        if (PassingStats.IsVisible)
        {
            stats.PassAtt = int.Parse(atts.Text);
            stats.PassComp = int.Parse(comps.Text);
            stats.PassYards = int.Parse(passyds.Text);
            stats.PassTDs = int.Parse(passtds.Text);
            stats.Interceptions = int.Parse(ints.Text);
        }
        if (RushingStats.IsVisible)
        {
            stats.RushAtt = int.Parse(rushatt.Text);
            stats.RecYards = int.Parse(rushyds.Text);
            stats.RushTDs = int.Parse(rushtds.Text);
            stats.Fumbles = int.Parse(fum.Text);
        }
        if (ReceivingStats.IsVisible)
        {
            stats.Targets = int.Parse(tar.Text);
            stats.Receptions = int.Parse(rec.Text);
            stats.RecYards = int.Parse(recyrds.Text);
            stats.RecTDs = int.Parse(rectds.Text);
        }
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