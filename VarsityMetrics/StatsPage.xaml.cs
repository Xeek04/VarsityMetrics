namespace VarsityMetrics;

public partial class StatsPage : ContentPage
{
    public StatsPage()
    {
        InitializeComponent();
    }
    private void AddStats(object sender, EventArgs e)
    {
        addplayerstat.IsVisible = false;
        StatLine.IsVisible = true;
        statnav.IsVisible = true;
    }
    private void BackButton(object sender, EventArgs e)
    {
        statnav.IsVisible = false;
        StatLine.IsVisible = false;
        addplayerstat.IsVisible = true;
        ClearLines();
    }
    private void ConfirmAdd(object sender, EventArgs e)
    {
        statnav.IsVisible = false;
        StatLine.IsVisible = false;
        addplayerstat.IsVisible = true;
        ClearLines();
    }
    private void FindStats(object sender, EventArgs e)
    {

    }
    private void ClearLines()
    {
        player.Text = string.Empty;
        comps.Text = string.Empty;
        atts.Text = string.Empty;
        passyds.Text = string.Empty;
        passtds.Text = string.Empty;
        ints.Text = string.Empty;
    }
}