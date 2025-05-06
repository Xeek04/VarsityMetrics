namespace VarsityMetrics;

using System.Diagnostics;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using VarsityMetrics.DB_Models;

public partial class PlaybookPage : ContentPage
{
	public List<PlayGroup> Plays;
    List<Play> OffensePlays;
    List<Play> DefensePlays;

    public PlaybookPage()
	{
		InitializeComponent();
		BindingContext = this;
        _ = InitShellAsync();
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
        //OffensePlays = await App.db.RequestPictureAsync("Offense");
        //DefensePlays = await App.db.RequestPictureAsync("Defense");
		//Plays = new List<PlayGroup>()
		//{
		//	OffensePlays as PlayGroup,
		//	DefensePlays as PlayGroup
		//};

		Plays = new List<PlayGroup>
		{
			new PlayGroup(true) //Offense
			{
				new Play
				{
					name = "Attack",
					times_called = 3,
					yards_gained = [3, 4, 5]
				}
			},
			new PlayGroup(false) //Defense
			{
                new Play
                {
                    name = "Defend",
                    times_called = 4
                }
            }
		};

		PlayList.ItemsSource= Plays;
	}


	private void AddButton_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new AddPlaybook());
	}

	private async void OrderPicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		//TODO implement
	}
	private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		var TypePicker = (Picker)sender;
		int selectedIndex = TypePicker.SelectedIndex;

		if (selectedIndex != -1)
		{
			if (TypePicker.Items[selectedIndex] == "Offense")
			{
				//TODO implement
			}
			else if (TypePicker.Items[selectedIndex] == "Defense")
            {
                //TODO implement
            }
            else
            {
                //TODO implement
            }
        }
	}

	private void DrawButton_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new DrawPlaybooks());
	}

	//private void OffenseList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	//{
	//	var selected = ((ListView)sender).SelectedItem as Plays;
	//	if (selected == null)
	//	{
	//		return;
	//	}

	//	//selected.Stats.Text() = "Testing";
    //}

    //private void DefenseList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    //{
    //    var selected = ((ListView)sender).SelectedItem as Plays;
    //    if (selected == null)
    //    {
    //        return;
    //    }

        //selected.Stats.Text() = "Testing";
    //}
	
	private async Task InitShellAsync()
	{
		Constants.Role? role = await App.db.GetCurrentUserRoleAsync();
		if (role == Constants.Role.Scout || role == Constants.Role.Player)
		{
			// AddButton.IsVisible = false;
			// DrawButton.IsVisible = false;
		}
	}

    private void StatsButton_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new AddPlaybookStats());
    }
}