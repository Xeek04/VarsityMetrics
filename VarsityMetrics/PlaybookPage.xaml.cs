namespace VarsityMetrics;

using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

public partial class PlaybookPage : ContentPage
{
	public PlaybookPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		OffenseList.ItemsSource = await App.db.RequestPictureAsync("Offense");
		DefenseList.ItemsSource = await App.db.RequestPictureAsync("Defense");
	}


	private void AddButton_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new AddPlaybook());
    }

    private async void OrderPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
		var OrderPicker = (Picker)sender;
		int selectedIndex = OrderPicker.SelectedIndex;

		if (selectedIndex != -1)
		{
			if (OrderPicker.Items[selectedIndex] == "Name")
			{
                OffenseList.ItemsSource = await App.db.RequestOrderedPictureAsync("Offense");
                DefenseList.ItemsSource = await App.db.RequestOrderedPictureAsync("Defense");
            }
			else
			{
                OffenseList.ItemsSource = await App.db.RequestPictureAsync("Offense");
                DefenseList.ItemsSource = await App.db.RequestPictureAsync("Defense");
            }
		}
    }
	private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		var TypePicker = (Picker)sender;
		int selectedIndex = TypePicker.SelectedIndex;

		if (selectedIndex != -1)
		{
			if (TypePicker.Items[selectedIndex] == "Offense")
			{
                Offense.IsVisible = true;
                OffenseList.IsVisible = true;
				Defense.IsVisible = false;
				DefenseList.IsVisible = false;
            }
			else if (TypePicker.Items[selectedIndex] == "Defense")
			{
                Defense.IsVisible = true;
                DefenseList.IsVisible = true;
				Offense.IsVisible = false;
				OffenseList.IsVisible = false;
            }
			else
			{
                Defense.IsVisible = true;
                Offense.IsVisible = true;
                DefenseList.IsVisible = true;
                OffenseList.IsVisible = true;
            }
		}
	}

    private void DrawButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new DrawPlaybooks());
    }
}