using VarsityMetrics.DB_Models;

namespace VarsityMetrics;

// TODO edit layout for small page size

public partial class SchedulePage : ContentPage
{
	public SchedulePage()
	{
		InitializeComponent();
        PopulateSchedule();
	}

    private void Back(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void EditScores(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GameStatEntryPage));
    }

    private void ShowEntries(object sender, EventArgs e)
    {
        gameEditor.IsVisible = !gameEditor.IsVisible;
        currentSchedule.IsVisible = !currentSchedule.IsVisible;
        inputEntries.IsVisible = false;
        addGame.IsVisible = currentSchedule.IsVisible == true ? false : true;
        ClearEntries();
        if (gameEditor.IsVisible) { EditView(); }
    }

    private async void PopulateSchedule()
    {
        List<Gamelog> schedule = await App.db.GetSchedule();
        int i = 1;
        if (currentSchedule.RowDefinitions.Count > 1)
        {
            for (int j = currentSchedule.Count; j > 0; j--)
            {
                currentSchedule.RemoveAt(j);
            }
            currentSchedule.RowDefinitions = new RowDefinitionCollection();
        }
        foreach (Gamelog game in schedule)
        {
            currentSchedule.AddRowDefinition(new RowDefinition());
            currentSchedule.AddWithSpan(new Label
            { 
                Text = game.ForTeam + " " + (game.HomeGame == true ? "vs" : "@") + " " + game.OppTeam,
                HorizontalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Italic
            },i,0,1,3);
            currentSchedule.Add(new Label 
            {
                Text = game.Date.ToString(),
                HorizontalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Italic
            },3,i);
            currentSchedule.Add(new Label 
            {
                Text = game.Location,
                HorizontalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Italic
            },4,i);
            i++;
        }
    }

    private async void EditView()
    {
        List<Gamelog> schedule = await App.db.GetSchedule();
        int i = 1;
        if (gameEditor.RowDefinitions.Count > 1)
        {
            for (int j = gameEditor.Count; j > 0; j--)
            {
                gameEditor.RemoveAt(j);
            }
            gameEditor.RowDefinitions = new RowDefinitionCollection();
        }
        foreach(Gamelog game in schedule)
        {
            gameEditor.AddRowDefinition(new RowDefinition());
            gameEditor.Add(new Entry 
            {
                Text = game.OppTeam,
                HorizontalOptions= LayoutOptions.Center,
                IsReadOnly = true
            },0,i);
            gameEditor.Add(new Entry 
            {
                Text = game.Date.ToString(),
                HorizontalOptions= LayoutOptions.Center,
                IsReadOnly = true
            },1,i);
            gameEditor.Add(new CheckBox 
            {
                IsChecked = game.HomeGame,
                HorizontalOptions= LayoutOptions.Center,
                IsEnabled = false
            },2,i);
            gameEditor.Add(new Entry
            {
                Text = game.Location,
                HorizontalOptions= LayoutOptions.Center,
                IsReadOnly = true
            },3,i);
            i++;
        }
    }

    private void ShowInput(object sender, EventArgs e)
    {
        inputEntries.IsVisible = true;
        addGame.IsVisible = false;
    }

    private async void Confirm(object sender, EventArgs e)
    {
        await App.db.AddGame(opp.Text ?? "null", date.Date, home.IsChecked, location.Text);
        inputEntries.IsVisible = false;
        addGame.IsVisible = true;
        ClearEntries();
        PopulateSchedule();
        EditView();
    }

    private void ClearEntries()
    {
        opp.Text = null;
        date.Date = DateTime.Now;
        home.IsChecked = false;
        location.Text = null;
    }

    private async void PlayByPlay(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GameStatEntryPage));
    }
}