namespace VarsityMetrics;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using VarsityMetrics.DB_Models;
using System.Text.RegularExpressions;

public class OffenseOnly : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string Type)
        {
            return Type == "Offense";
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
public partial class PlaybookPage : ContentPage
{
    public List<Play> Plays;
    public IEnumerable<PlayView> PlayViews;
    public IEnumerable<PlayGroup> Playgroups;
    List<Play> OffensePlays;
    List<Play> DefensePlays;

    public string Order = "Default";
    public string TypeSelector = "All";

    public ObservableCollection<PlayGroup> GroupedPlays { get; set; } = new();

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

        /*Plays = new List<List<PlayGroup>>
		{
			new List<Play>
			{
                new PlayGroup(true), //Offense

                await App.db.RequestPictureAsync("Offense")
            }
			new PlayGroup(true) //Offense
			{
				*//*new Play
				{
					name = "Attack",
					times_called = 3,
					yards_gained = [3, 4, 5]
				}*//*
				await App.db.RequestPictureAsync("Offense")

            },
			new PlayGroup(false) //Defense
			{
                await App.db.RequestPictureAsync("Defense")
            }
		};*/

        Plays = await App.db.RequestPictureAsync();

        PlayViews = Plays.Select(p => new PlayView(p));

        Playgroups = PlayViews.GroupBy(p => p.type).OrderBy(g => g.Key == "Offense" ? 0 : 1).Select(g => new PlayGroup(g.Key, g));

        /*GroupedPlays.Clear();
		foreach ( var group in grouped)
			GroupedPlays.Add(group);*/


        PlayList.ItemsSource = Playgroups;
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

            PlayViews = Plays.Select(p => new PlayView(p));

            if (OrderPicker.Items[selectedIndex] == "Default")
            {
                Order = "Default";

                Playgroups = PlayViews.GroupBy(p => p.type)
                    .OrderBy(g => g.Key == "Offense" ? 0 : 1)
                    .Select(g => new PlayGroup(g.Key, g));

                if (String.Equals(TypeSelector, "Offense"))
                {
                    Playgroups = PlayViews.Where(p => p.type == "Offense")
                        .GroupBy(p => p.type)
                        .Select(g => new PlayGroup(g.Key, g))
                        .ToList();
                }
                else if (String.Equals(TypeSelector, "Defense"))
                {
                    Playgroups = PlayViews.Where(p => p.type == "Defense")
                        .GroupBy(p => p.type)
                        .Select(g => new PlayGroup(g.Key, g))
                        .ToList();
                }


                /*GroupedPlays.Clear();
                foreach (var group in grouped)
                    GroupedPlays.Add(group);*/


                PlayList.ItemsSource = Playgroups;
            }
            else
            {
                Order = "Name";

                Playgroups = PlayViews.GroupBy(p => p.type)
                    .OrderBy(g => g.Key == "Offense" ? 0 : 1)
                    .Select(g => new PlayGroup(g.Key, g.OrderBy(p => p.Name)));

                if (String.Equals(TypeSelector, "Offense"))
                {
                    Playgroups = PlayViews.Where(p => p.type == "Offense")
                        .GroupBy(p => p.type)
                        .Select(g => new PlayGroup(g.Key, g.OrderBy(p => p.Name)))
                        .ToList();
                }
                else if (String.Equals(TypeSelector, "Defense"))
                {
                    Playgroups = PlayViews.Where(p => p.type == "Defense")
                        .GroupBy(p => p.type)
                        .Select(g => new PlayGroup(g.Key, g.OrderBy(p => p.Name)))
                        .ToList();
                }

                /*GroupedPlays.Clear();
                foreach (var group in grouped)
                    GroupedPlays.Add(group);*/


                PlayList.ItemsSource = Playgroups;
            }
        }
    }
    private async void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var TypePicker = (Picker)sender;
        int selectedIndex = TypePicker.SelectedIndex;

        if (selectedIndex != -1)
        {

            PlayViews = Plays.Select(p => new PlayView(p));

            if (TypePicker.Items[selectedIndex] == "Offense")
            {
                TypeSelector = "Offense";

                Playgroups = PlayViews.Where(p => p.type == "Offense")
                    .GroupBy(p => p.type)
                    .Select(g => new PlayGroup(g.Key, g))
                    .ToList();
                if (String.Equals(Order, "Name"))
                {
                    Playgroups = PlayViews.Where(p => p.type == "Offense")
                        .GroupBy(p => p.type)
                        .Select(g => new PlayGroup(g.Key, g.OrderBy(p => p.Name)))
                        .ToList();
                }

                /*GroupedPlays.Clear();
                foreach (var group in grouped)
                    GroupedPlays.Add(group);*/


                PlayList.ItemsSource = Playgroups;
            }
            else if (TypePicker.Items[selectedIndex] == "Defense")
            {
                TypeSelector = "Defense";

                Playgroups = PlayViews.Where(p => p.type == "Defense")
                    .GroupBy(p => p.type)
                    .Select(g => new PlayGroup(g.Key, g.OrderBy(p => p.Name)))
                    .ToList();

                if (String.Equals(Order, "Name"))
                {
                    Playgroups = PlayViews.Where(p => p.type == "Defense")
                        .GroupBy(p => p.type)
                        .Select(g => new PlayGroup(g.Key, g.OrderBy(p => p.Name)))
                        .ToList();
                }



                /*GroupedPlays.Clear();
                foreach (var group in grouped)
                    GroupedPlays.Add(group);*/


                PlayList.ItemsSource = Playgroups;
            }
            else
            {
                TypeSelector = "All";

                Playgroups = PlayViews.GroupBy(p => p.type)
                    .OrderBy(g => g.Key == "Offense" ? 0 : 1)
                    .Select(g => new PlayGroup(g.Key, g));

                if (String.Equals(Order, "Name"))
                {
                    Playgroups = PlayViews.GroupBy(p => p.type)
                        .OrderBy(g => g.Key == "Offense" ? 0 : 1)
                        .Select(g => new PlayGroup(g.Key, g.OrderBy(p => p.Name)));
                }

                /*GroupedPlays.Clear();
                foreach (var group in grouped)
                    GroupedPlays.Add(group);*/


                PlayList.ItemsSource = Playgroups;
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

    private async void PlayList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not PlayView selectedPlay)
            return;

        if (selectedPlay.type == "Defense")
        {
            PlayList.SelectedItem = null;
            return;
        }

        string input = await DisplayPromptAsync(
            "Add Yardage",
            $"Enter yards gained for '{selectedPlay.Name}':",
            "Add",
            "Cancel",
            "e.g. 5",
            keyboard: Keyboard.Numeric
        );

        if (int.TryParse(input, out int newYards))
        {
            // Update play data
            var yardsList = selectedPlay.Base.yards_gained?.ToList() ?? new List<int>();
            yardsList.Add(newYards);
            selectedPlay.Base.yards_gained = yardsList.ToArray();
            selectedPlay.Base.times_called++;

            // Update DB
            await App.db.UpdatePlayAscync(selectedPlay.Base);

            // Refresh the display with a full re-bind
            PlayViews = Plays.Select(p => new PlayView(p));

            if (TypeSelector == "Offense")
                PlayViews = PlayViews.Where(p => p.type == "Offense");
            else if (TypeSelector == "Defense")
                PlayViews = PlayViews.Where(p => p.type == "Defense");

            if (Order == "Name")
            {
                Playgroups = PlayViews
                    .GroupBy(p => p.type)
                    .OrderBy(g => g.Key == "Offense" ? 0 : 1)
                    .Select(g => new PlayGroup(g.Key, g.OrderBy(p => p.Name)))
                    .ToList();
            }
            else
            {
                Playgroups = PlayViews
                    .GroupBy(p => p.type)
                    .OrderBy(g => g.Key == "Offense" ? 0 : 1)
                    .Select(g => new PlayGroup(g.Key, g))
                    .ToList();
            }

            PlayList.ItemsSource = null; // Force refresh
            PlayList.ItemsSource = Playgroups;
        }

        PlayList.SelectedItem = null;
    }

}