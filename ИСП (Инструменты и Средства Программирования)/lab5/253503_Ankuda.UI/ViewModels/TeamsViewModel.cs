using _253503_Ankuda.Applicationn.TeamUseCases.Queries.GetAllTeams;
using _253503_Ankuda.Applicationn.CompetitorUseCases.Queries.GetCompetitorsByTeam;
using _253503_Ankuda.UI.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _253503_Ankuda.UI.ViewModels
{
    public partial class TeamsViewModel : ObservableObject
    {

        private readonly IMediator _mediator;
        public TeamsViewModel(IMediator mediator)
        {
            Competitors.Clear();
            _mediator = mediator;
        }
        public ObservableCollection<TeamEntity> Teams { get; set; } = new();
        public ObservableCollection<CompetitorEntity> Competitors { get; set; } = new();
        [ObservableProperty]
        TeamEntity selectedTeam;
        [RelayCommand]
        async Task UpdateTeamsList() => await GetTeams();
        [RelayCommand]
        async Task UpdateCompetitorsList() => await GetCompetitors();
        [RelayCommand]
        async void ShowDetails(CompetitorEntity competitor) => await GotoDetailsPage(competitor);
        public async Task GetTeams()
        {
            Competitors.Clear();
            var teams = await _mediator.Send(new GetAllTeamsRequest());
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Teams.Clear();
                foreach (var team in teams)
                    Teams.Add(team);
            });
        }
        public async Task GetCompetitors()
        {
            if (SelectedTeam == null) { return; }
            var competitors = await _mediator.Send(new GetCompetitorsByTeamRequest(SelectedTeam.Id));
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Competitors.Clear();
                foreach (var competitor in competitors)
                    Competitors.Add(competitor);
            });
        }
        private async Task GotoDetailsPage(CompetitorEntity competitor)
        {
            Competitors.Clear();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 64
            };
            var serializedCompetitor = JsonSerializer.Serialize(competitor, options);
            await Shell.Current.GoToAsync($"{nameof(CompetitorDetails)}?task={Uri.EscapeDataString(serializedCompetitor)}");
        }
    }
}

