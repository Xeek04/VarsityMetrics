using System.Diagnostics;

namespace VarsityMetrics;

public partial class AddPlaybookStats : ContentPage
{
	public AddPlaybookStats()
	{
		InitializeComponent();
	}

    private void cancel_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void confirm_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine("Confirm clicked");
        int err = 0;
        NameError.IsVisible = false;
        TypeError.IsVisible = false;
        YardsError.IsVisible = false;

        if (name.Text == null | String.Equals(name.Text, ""))
        {
            NameError.Text = "Please fill in";
            NameError.IsVisible = true;
            err = 1;
        }

        int selectedIndex = TypePicker.SelectedIndex;

        if (selectedIndex == -1)
        {
            TypeError.Text = "Please select an option";
            TypeError.IsVisible = true;
            err = 1;
        }

        if(YardsGained.Text ==  null | String.Equals(YardsGained.Text, ""))
        {
            YardsError.Text = "Please fill in";
            YardsError.IsVisible = true;
            err = 1;
        }
        else if(!int.TryParse(YardsGained.Text, out int result))
        {
            Debug.WriteLine("not a number");
            YardsError.Text = "Please input a number";
            YardsError.IsVisible = true;
            err = 1;
        }
        Debug.WriteLine("outside");
        Debug.WriteLine(err);
        if (err == 0)
        {
            Debug.WriteLine("No err");
            int yards = int.Parse(YardsGained.Text);
            string type = (string)TypePicker.ItemsSource[selectedIndex];
            var stats = await App.db.AddPlaybookStats(name.Text, type, yards);
            Debug.WriteLine(stats);
            if (stats)
            {
                Navigation.PopAsync();
            }
        }

    }
}