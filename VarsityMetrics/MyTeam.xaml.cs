namespace VarsityMetrics;

public partial class MyTeam : ContentPage
{
	public MyTeam()
	{
		InitializeComponent();
        MessagingCenter.Subscribe<AddTeamPlayer, string>(this, "AddLabel", (sender, arg) =>
        {
            var newLabel = new Label
            {
                Text = arg,
                FontSize = 18,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            LayoutVerticale.Children.Add(newLabel);
        });
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddTeamPlayer());
    }
}