using _253503_Ankuda.Applicationn.TeamUseCases.Queries.GetAllTeams;
using _253503_Ankuda.Applicationn.CompetitorUseCases.Commands.AddCompetitor;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.UI.ViewModels
{
    public partial class CompetitorAddingViewModel : ObservableObject
    {
        private string _competitorName = string.Empty;
        private string _competitorDescription = string.Empty;
        private readonly IMediator _mediator;
        private string _notifyString = string.Empty;
        private string _notifyStringColor = "White";
        public ObservableCollection<TeamEntity> Teams { get; set; } = new();
        [ObservableProperty]
        TeamEntity? selectedTeam = null;
        public CompetitorAddingViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public string CompetitorName
        {
            get { return _competitorName; }
            set { _competitorName = value; OnPropertyChanged(); }
        }
        public string CompetitorDescription
        {
            get { return _competitorDescription; }
            set { _competitorDescription = value; OnPropertyChanged(); }
        }
        public string NotifyString
        {
            get { return _notifyString; }
            set { _notifyString = value; OnPropertyChanged(); }
        }
        public string NotifyStringColor
        {
            get { return _notifyStringColor; }
            set { _notifyStringColor = value; OnPropertyChanged(); }
        }
        [RelayCommand]
        async Task AddButtonClicked() => await AddCompetitor();

        [RelayCommand]
        async Task UpdateTeamsList() => await GetTeams();

        public async Task GetTeams()
        {
            var teams = await _mediator.Send(new GetAllTeamsRequest());
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Teams.Clear();
                foreach (var team in teams)
                    Teams.Add(team);
            });
        }

        public async Task AddCompetitor()
        {
            if (CompetitorName == string.Empty) { NotifyString = "Name is Empty"; NotifyStringColor = "Red"; return; }
            if (CompetitorDescription == string.Empty) { NotifyString = "Description is Empty"; NotifyStringColor = "Red"; return; }
            if (selectedTeam == null) { NotifyString = "Choose a team!"; NotifyStringColor = "Red"; return; }
            await _mediator.Send(new AddCompetitorRequest(CompetitorName, CompetitorDescription, selectedTeam.Id));
            NotifyString = "Task added successfully";
            NotifyStringColor = "Green";
            CompetitorName = string.Empty;
            CompetitorDescription = string.Empty;
        }
    }
}