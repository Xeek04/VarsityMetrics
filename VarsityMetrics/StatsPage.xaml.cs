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
        //back.IsVisible = true;
        //confirm.IsVisible = true;
    }
    private void BackButton(object sender, EventArgs e)
    {
        //back.IsVisible = false;
        //confirm.IsVisible = false;
        statnav.IsVisible = false;
        StatLine.IsVisible = false;
        addplayerstat.IsVisible = true;
    }
    private void ConfirmAdd(object sender, EventArgs e)
    {
        //back.IsVisible = false;
        //confirm.IsVisible = false;
        statnav.IsVisible = false;
        StatLine.IsVisible = false;
        addplayerstat.IsVisible = true;
    }
    private void FindStats(object sender, EventArgs e)
    {

    }
}