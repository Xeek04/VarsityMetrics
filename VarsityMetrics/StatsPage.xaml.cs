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
        if (playerList.SelectedItem != null)
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
    }

    private void StatSelection(object sender, EventArgs e)
    {
        passbutton.IsVisible = false;
        rushbutton.IsVisible = false;
        recbutton.IsVisible = false;
        statnav.IsVisible = true;
        confirm.IsVisible = false;
        
        addplayerstats.IsVisible = false;
        playerListStats.IsVisible = true;
    }

    private async void ShowStats(object sender, EventArgs e)
    {
        if (playerListStats.SelectedItem != null)
        {
            string[] player = playerListStats.SelectedItem.ToString().Replace(",", null).Split();
            PlayerStats stats = await App.db.StatQuery(player[1], player[0]);
            PassingView.IsVisible = false;
            RushingView.IsVisible = false;
            RecView.IsVisible = false;

            if (stats.PassAtt != null)
            {
                attsV.Text = stats.PassAtt.ToString();
                compV.Text = stats.PassComp.ToString();
                compperV.Text = (Math.Round(((double)stats.PassComp / (double)stats.PassAtt) * 100)).ToString();
                passydsV.Text = stats.PassYards.ToString();
                passtdsV.Text = stats.PassTDs.ToString();
                intsV.Text = stats.Interceptions.ToString();
                ypaV.Text = (Math.Round((double)stats.PassYards / (double)stats.PassAtt)).ToString();
                PassingView.IsVisible = true;
            }
            if (stats.RushAtt != null)
            {
                rushattV.Text = stats.RushAtt.ToString();
                rushydsV.Text = stats.RushYards.ToString();
                rushtdsV.Text = stats.RushTDs.ToString();
                fumV.Text = stats.Fumbles.ToString();
                ypcV.Text = (Math.Round((double)stats.RushYards / (double)stats.RushAtt)).ToString();
                RushingView.IsVisible = true;
            }
            if (stats.Targets != null)
            {
                tarV.Text = stats.Targets.ToString();
                tarV.Text = stats.Targets.ToString();
                recV.Text = stats.Receptions.ToString();
                recyrdsV.Text = stats.RecYards.ToString();
                rectdsV.Text = stats.RecTDs.ToString();
                yprV.Text = (Math.Round((double)stats.RecYards / (double)stats.Receptions)).ToString();
                RecView.IsVisible = true;
            }
        }
    }

    private void AddStats(object sender, EventArgs e)
    {
        statnav.IsVisible = true;
        playerList.IsVisible = true;
        passbutton.IsVisible = true;
        rushbutton.IsVisible = true;
        recbutton.IsVisible = true;
        showplayerstats.IsVisible = false;
        confirm.IsVisible = true;
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
            stats.RushYards = int.Parse(rushyds.Text);
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
        confirm.IsVisible = true;
        playerList.IsVisible = false;
        playerListStats.IsVisible = false;
        PassingStats.IsVisible = false;
        RushingStats.IsVisible = false;
        ReceivingStats.IsVisible = false;
        passbutton.IsVisible = false;
        rushbutton.IsVisible = false;
        recbutton.IsVisible = false;
        showplayerstats.IsVisible = true;
        addplayerstats.IsVisible = true;
        PassingView.IsVisible = false;
        RushingView.IsVisible = false;
        RecView.IsVisible = false;
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

        playerList.SelectedItem = null;
        playerListStats.SelectedItem = null;
    }
}