using VarsityMetrics.ViewModels;
using VarsityMetrics.DB_Models;
using System.Runtime.CompilerServices;
namespace VarsityMetrics;

public partial class GameStatEntryPage : ContentPage
{
    private Dictionary<string, int> gameKey = new Dictionary<string, int>();
    private Dictionary<string, int> playKey = new Dictionary<string, int>();
  	private Dictionary<int, Grid> quarterKey = new Dictionary<int, Grid>();
    private Dictionary<int?, string> downKey = new Dictionary<int?, string>();
    
    public GameStatEntryPage()
	{
        List<int> pickerNum = new List<int> { 1, 2, 3, 4 };
        List<string> types = new List<string> { "Offense","Defense","Special Teams" };
        List<string> pass_run = new List<string> { "Pass", "Run" };
        InitializeComponent();
        PopulateGames();
        GetPlays();

        down.ItemsSource = pickerNum;
        quarter.ItemsSource = pickerNum;
        //playType.ItemsSource = types;
        yardageType.ItemsSource = pass_run;

        quarterKey.Add(1, Q1);
        quarterKey.Add(2, Q2);
        quarterKey.Add(3, Q3);
        quarterKey.Add(4, Q4);

        downKey.Add(1, " 1st down");
        downKey.Add(2, " 2nd down");
        downKey.Add(3, " 3rd down");
        downKey.Add(4, " 4th down");
	}

    private async void GetPlays()
    {

        List<string> playList = new List<string>();
        List<Play> plays = await App.db.GetPlays();
        foreach (Play play in plays)
        {
            playList.Add(play.name);
            playKey.Add(play.name, play.play_id);
        }
        playBook.ItemsSource = playList;
    }

    private void Back(object sender, EventArgs e)
    {
        Navigation.PopAsync();
        PopulateGames();
        GetPlays();
    }

    private async void PopulateGames()
    {
        gameKey.Clear();
        List<Gamelog> gamelog = await App.db.GetSchedule();
        List<string> result = new List<string>();
        foreach (Gamelog game in gamelog)
        {
            if (!game.StatsEntered) 
            {
                result.Add(game.OppTeam);
                gameKey.Add(game.OppTeam, game.GameId);
            }
        }
        games.ItemsSource = result;
    }

    private void ItemChanged(object sender, EventArgs e)
    {
        if (games.SelectedItem == null) 
        { 
            start.Background = Brush.Gray;
            start.IsEnabled = false;
        }
        else 
        { 
            start.Background = Brush.Green; 
            start.IsEnabled = true;
        }
        ClearPlays(Q1);
        ClearPlays(Q2);
        ClearPlays(Q3);
        ClearPlays(Q4);
        PopulatePlays();
    }

    private void ClearPlays(Grid quarter)
    {
        if (quarter.RowDefinitions.Count > 1)
        {
            for (int j = quarter.Count; j > 0; j--)
            {
                quarter.RemoveAt(j);
            }
            quarter.RowDefinitions = new RowDefinitionCollection();
        }
    }

    private void StartEntry(object sender, EventArgs e)
    {
        navbar.IsVisible = false;
        submit.IsVisible = true;
        entryView.IsVisible = true;
    }

    private void SubmitEntry(object sender, EventArgs e)
    {
        navbar.IsVisible = true;
        submit.IsVisible = false;
        entryView.IsVisible = false;
    }

    private async void PopulatePlays()
    {
        Q1S.IsVisible = false;
        Q2S.IsVisible = false;
        Q3S.IsVisible = false;
        Q4S.IsVisible = false;
        List<PlayByPlay> Q1plays = await App.db.GetPlayByPlay(gameKey.GetValueOrDefault(games.SelectedItem.ToString()),1);
        List<PlayByPlay> Q2plays = await App.db.GetPlayByPlay(gameKey.GetValueOrDefault(games.SelectedItem.ToString()),2);
        List<PlayByPlay> Q3plays = await App.db.GetPlayByPlay(gameKey.GetValueOrDefault(games.SelectedItem.ToString()),3);
        List<PlayByPlay> Q4plays = await App.db.GetPlayByPlay(gameKey.GetValueOrDefault(games.SelectedItem.ToString()),4);
        
        if (Q1plays.Count>0)
        {
            int i = 0;
            foreach (var play in Q1plays)
            {
                PlayBuilder(play, i, 1, Q1);
                i++;
            }
            Q1S.IsVisible = true;
        }
        if (Q2plays.Count > 0)
        {
            int i = 0;
            foreach (var play in Q2plays)
            {
                PlayBuilder(play, i, 3, Q2);
                i++;
            }
            Q2S.IsVisible = true;
        }        
        if (Q3plays.Count > 0)
        {
            int i = 0;
            foreach (var play in Q3plays)
            {
                PlayBuilder(play, i, 3, Q3);
                i++;
            }
            Q3S.IsVisible = true;
        }        
        if (Q4plays.Count > 0)
        {
            int i = 0;
            foreach (var play in Q4plays)
            {
                PlayBuilder(play, i, 4, Q4);
                i++;
            }
            Q4S.IsVisible = true;
        }
    }

    private string PlayTextBuilder(PlayByPlay play)
    {
        string result = play.Players[0];
        if (play.YardType == "Pass") { result += " pass to " + play.Players[1]; }
        else { result += " rush"; }
        result += " for " + play.Yards + " yards";
        return result;
    }

    private void PlayBuilder(PlayByPlay play, int i, int quarter, Grid section)
    {
        section.AddRowDefinition(new RowDefinition());
        section.Add(new Label
        {
            Text = play.GameTime + downKey.GetValueOrDefault(play.Down),
            FontAttributes = FontAttributes.Italic
        },0,i);
        section.Add(new Label
        {
            Text = PlayTextBuilder(play)
        },1,i);
    }

    private async void EnterPlay(object sender, EventArgs e)
    {
        PlayByPlay play = new PlayByPlay
        {
            GameId = gameKey.GetValueOrDefault(games.SelectedItem.ToString()),
            Down = int.Parse(down.SelectedItem.ToString()),
            Quarter = int.Parse(quarter.SelectedItem.ToString()),
            GameTime = gameClock.Text,
            YardType = yardageType.SelectedItem.ToString(),
            Yards = int.Parse(yards.Text),
            PlayId = playKey.GetValueOrDefault(playBook.SelectedItem.ToString())
        };
        if (yardageType.SelectedItem.ToString() == "Pass")
        {
            play.Players = [player1.Text, player2.Text];
        }
        else { play.Players = [player1.Text]; }
        await App.db.AddPlayToGame(play);
        CheckQ(play.Quarter);
        PlayBuilder(play, quarterKey.GetValueOrDefault(play.Quarter).RowDefinitions.Count, play.Quarter, quarterKey.GetValueOrDefault(play.Quarter));
        await App.db.AddPlaybookStats(playBook.SelectedItem.ToString(),"Offense",int.Parse(yards.Text));
    }

    private void CheckQ(int q)
    {
        VerticalStackLayout curr = null;
        switch (q){
            case 1:
                curr = Q1S;
                break;
            case 2:
                curr = Q2S;
                break;
            case 3:
                curr = Q3S;
                break;
            case 4:
                curr = Q4S;
                break;
            default:
                break;
        }
        if(!curr.IsVisible) { curr.IsVisible = true; }
    }
}