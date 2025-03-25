namespace VarsityMetrics;

public partial class AddTeamPlayer: ContentPage
{
	public AddTeamPlayer()
	{
		InitializeComponent();
	}

    private void Exit_Page(object sender, EventArgs e)
    {
        
        string enteredText = PlayerName.Text;

        if (!string.IsNullOrEmpty(enteredText))
        {
            
            MessagingCenter.Send(this, "AddLabel", enteredText);
        }
        Navigation.PopAsync();
    }
}