using System.Collections.ObjectModel;
using VarsityMetrics.DB_Models;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace VarsityMetrics;

public partial class MyTeam : ContentPage
{
    private int? currentTeamId;
    private ObservableCollection<Teams> TeamMembers = new();

    private string? currentUserEmail => DBAccess.client.Auth.CurrentUser?.Email;
    public MyTeam()
	{
        InitializeComponent();
        TeamMembersCollectionView.ItemsSource = TeamMembers;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Ensure that the user is logged in and has an email
        if (string.IsNullOrWhiteSpace(currentUserEmail))
        {
            await DisplayAlert("Error", "No user is currently logged in.", "OK");
            return;
        }

        // Check if the current user is already in a team
        var existingTeam = await DBAccess.client
            .From<Teams>()
            .Where(x => x.Email == currentUserEmail)
            .Get();

        var teamEntry = existingTeam.Models.FirstOrDefault();
        if (teamEntry != null)
        {
            // User is already in a team, show team info and hide create team button
            currentTeamId = teamEntry.Team_Id;
            CreateTeamButton.IsVisible = false;
            await LoadTeamMembers();
        }
        else
        {
            // User is not in a team, allow them to create a team
            CreateTeamButton.IsVisible = true;
        }
    }

    private async void CreateTeamButton_Clicked(object sender, EventArgs e)
    {
        var teamName = await DisplayPromptAsync("Create Team", "Enter team name:");
        if (string.IsNullOrWhiteSpace(teamName)) return;

        var newTeam = new School_Teams { Name = teamName };
        var response = await DBAccess.client.From<School_Teams>().Insert(newTeam);
        var createdTeam = response.Models.FirstOrDefault();

        if (createdTeam != null)
        {
            var selfEntry = new Teams
            {
                Team_Id = createdTeam.Id,
                Email = currentUserEmail,
                Role = "Coach",
            };
            await DBAccess.client.From<Teams>().Insert(selfEntry);

            currentTeamId = createdTeam.Id;
            CreateTeamButton.IsVisible = false;
            await DisplayAlert("Success", "Team created.", "OK");
            await LoadTeamMembers();
        }
    }
    private async void AddMemberButton_Clicked(object sender, EventArgs e)
    {
        if (currentTeamId == null)
        {
            await DisplayAlert("Error", "Create or select a team first.", "OK");
            return;
        }

        var email = await DisplayPromptAsync("Add Member", "Enter user email:");
        var role = await DisplayPromptAsync("Add Member", "Enter role:");

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(role)) return;

        var accountQuery = await DBAccess.client
            .From<DB_Models.Accounts>()
            .Where(x => x.Email == email)
            .Get();

        if (!accountQuery.Models.Any())
        {
            await DisplayAlert("Error", "User does not have an account.", "OK");
            return;
        }
        var existingTeamQuery = await DBAccess.client
            .From<Teams>()
            .Where(x => x.Email == email)
            .Get();

        if (existingTeamQuery.Models.Any())
        {
            await DisplayAlert("Error", "User is already in a team.", "OK");
            return;
        }

        var newMember = new Teams
        {
            Team_Id = currentTeamId.Value,
            Email = email,
            Role = role
        };

        await DBAccess.client.From<Teams>().Insert(newMember);
        await LoadTeamMembers();
    }
    private async void EditRoleButton_Clicked(object sender, EventArgs e)
    {
        
    }

    private async Task LoadTeamMembers()
    {
        if (currentTeamId == null) return;

        var teamQuery = await DBAccess.client
            .From<Teams>()
            .Where(x => x.Team_Id == currentTeamId.Value)
            .Get();

        TeamMembers.Clear();
        foreach (var member in teamQuery.Models)
            TeamMembers.Add(member);
    }




  
}