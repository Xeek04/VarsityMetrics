using System.Windows.Input;
using VarsityMetrics.ViewModels;
using VarsityMetrics.DB_Models;

namespace VarsityMetrics;

public partial class RosterPage : ContentPage
{
    private readonly RosterViewModel _viewModel;

    private Label FnameLabel = new Label { Text = "First Name" };
    private Label LnameLabel = new Label { Text = "Last Name" };
    private Label HeightLabel = new Label { Text = "Height" };
    private Label WeightLabel = new Label { Text = "Weight" };
    private Label NumberLabel = new Label { Text = "No." };
    private Button Confirm = new Button { Text = ">", CommandParameter = null };

    private Label entryError = new Label { Text = "Please fill out all the entries.", TextColor = Color.FromArgb("#FF0000"), IsVisible = true };

    private Entry Fname = new Entry();
    private Entry Lname = new Entry();
    private Entry HeightP = new Entry();
    private Entry Weight = new Entry();
    private Entry Number = new Entry();

    private Dictionary<Button, Grid> gridKey = new Dictionary<Button, Grid>();
    private Dictionary<Grid, string> viewKey = new Dictionary<Grid, string>();
    public RosterPage()
	{
		InitializeComponent();
        _viewModel = new RosterViewModel();
        BindingContext = _viewModel;
        _ = InitShellAsync();

        gridKey.Add(QBButton, addQB);
        gridKey.Add(RBButton, addRB);
        gridKey.Add(WRButton, addWR);
        gridKey.Add(TEButton, addTE);
        gridKey.Add(OLButton, addOL);
        gridKey.Add(DEButton, addDE);
        gridKey.Add(DTButton, addDT);
        gridKey.Add(LBButton, addLB);
        gridKey.Add(CBButton, addCB);
        gridKey.Add(SButton, addS);

        viewKey.Add(QBList, "QB");
        viewKey.Add(RBList, "RB");
        viewKey.Add(WRList, "WR");
        viewKey.Add(TEList, "TE");
        viewKey.Add(OLList, "OL");
        viewKey.Add(DEList, "DE");
        viewKey.Add(DTList, "DT");
        viewKey.Add(LBList, "LB");
        viewKey.Add(CBList, "CB");
        viewKey.Add(SList, "S");

        foreach (Grid positionView in viewKey.Keys)
        {
            populateRoster(positionView);
        }

        Confirm.Clicked += async (sender, args) => addPlayer(Confirm.CommandParameter.ToString());
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.Window.MinimumHeight = 300;
        this.Window.MinimumWidth = 700;
    }
    private async void addPlayer(string position)
    {
        int err = 0;

        if(Fname.Text == null | String.Equals(Fname.Text, ""))
        {
            err = 1;
        }
        if (Lname.Text == null | String.Equals(Lname.Text, ""))
        {
            err = 1;
        }
        if (HeightP.Text == null | String.Equals(HeightP.Text, ""))
        {
            err = 1;
        }
        if (Weight.Text == null | String.Equals(Weight.Text, ""))
        {
            err = 1;
        }
        if (Number.Text == null | String.Equals(Number.Text, ""))
        {
            err = 1;
        }

        if(err == 0)
        {
            entryError.IsVisible = false;

            await App.db.AddPlayer(Fname.Text, Lname.Text, position, HeightP.Text, Weight.Text, int.Parse(Number.Text));
            List<Roster> something = await App.db.GetRoster();

            populateRoster(viewKey.FirstOrDefault(x => x.Value == position).Key);
            //var test = await App.db.StatQuery(Fname.Text, Lname.Text);
            clearEntries();
        }
        else
        {
            entryError.IsVisible = true;
        }
    }

    private async void clearRoster(object sender, EventArgs e)
    {
        await App.db.ClearRoster();
        var something = await App.db.GetRoster();
        foreach (Grid positionView in viewKey.Keys)
        {
            populateRoster(positionView);
        }
    }

    private void entryLine(object sender, EventArgs e)
    {
        Button addButton = (Button)sender;
        Grid view = gridKey.GetValueOrDefault(addButton);
        if (view.Count == 1)
        {
            if(Fname.Parent != null)
            {
                clearGrid((Grid)Fname.Parent);
            }
            view.Add(FnameLabel, 0, 0);
            view.Add(LnameLabel, 1, 0);
            view.Add(HeightLabel, 2, 0);
            view.Add(WeightLabel, 3, 0);
            view.Add(NumberLabel, 4, 0);
            view.Add(Confirm, 5, 0);

            view.Add(Fname, 0, 1);
            view.Add(Lname, 1, 1);
            view.Add(HeightP, 2, 1);
            view.Add(Weight, 3, 1);
            view.Add(Number, 4, 1);

            //view.Add(entryError, 5, 2);
            //Check other pages for error handling on empty entries

            clearEntries();
            Confirm.CommandParameter = addButton.CommandParameter;
        }
        else
        {
            clearGrid(view);
        }
    }

    private void clearEntries()
    {
        Fname.Text = "";
        Lname.Text = "";
        HeightP.Text = "";
        Weight.Text = "";
        Number.Text = "";
    }

    private void clearGrid(Grid view)
    {
        view.Remove(FnameLabel);
        view.Remove(LnameLabel);
        view.Remove(HeightLabel);
        view.Remove(WeightLabel);
        view.Remove(NumberLabel);
        view.Remove(Confirm);

        view.Remove(Fname);
        view.Remove(Lname);
        view.Remove(HeightP);
        view.Remove(Weight);
        view.Remove(Number);
    }
    private void openEditRoster(object sender, EventArgs e)
    {
        addQB.IsVisible = !addQB.IsVisible;
        addRB.IsVisible = !addRB.IsVisible;
        addWR.IsVisible = !addWR.IsVisible;
        addTE.IsVisible = !addTE.IsVisible;
        addOL.IsVisible = !addOL.IsVisible;
        addDE.IsVisible = !addDE.IsVisible;
        addDT.IsVisible = !addDT.IsVisible;
        addLB.IsVisible = !addLB.IsVisible;
        addCB.IsVisible = !addCB.IsVisible;
        addS.IsVisible = !addS.IsVisible;

        if ((Grid)Fname.Parent != null) { clearGrid((Grid)Fname.Parent); }
        clearEntries();
    }

    private async void populateRoster(Grid positionView)
    {
        List<Roster> positionRoster = await App.db.GetRosterByPosition(viewKey.GetValueOrDefault(positionView));
        int i = 0;
        if (positionView.RowDefinitions.Count>0)
        {
            for (int j = positionView.Count; j > -1; j--)
            {
                positionView.RemoveAt(j);
            }
            positionView.RowDefinitions = new RowDefinitionCollection();
        }
        foreach (Roster player in positionRoster)
        {
            positionView.AddRowDefinition(new RowDefinition());
            positionView.Add(new Entry 
            {
                Text = player.Fname + " " + player.Lname,
                IsReadOnly = true
            },0,i);
            positionView.Add(new Entry 
            {
                Text = player.Height,
                IsReadOnly = true
            },1,i);
            positionView.Add(new Entry 
            {
                Text = player.Weight,
                IsReadOnly = true
            },2,i);
            positionView.Add(new Entry 
            {
                Text = player.Number.ToString(),
                IsReadOnly = true
            },3,i);
            // button link to stats
            positionView.AddWithSpan(new Button
            {
                BackgroundColor = new Color(0, 0, 0, 0),
                Command = _viewModel.OpenStatsCommand,
                CommandParameter = player

            }, i, 0, 1, positionView.ColumnDefinitions.Count);
            i++;
        }
    }
    private async Task InitShellAsync()
    {
        Constants.Role? role = await App.db.GetCurrentUserRoleAsync();
        if (role == Constants.Role.Scout || role == Constants.Role.Player)
        {
            // EditButton.IsVisible = false;
            // ClearButton.IsVisible = false;
        }
    }
}