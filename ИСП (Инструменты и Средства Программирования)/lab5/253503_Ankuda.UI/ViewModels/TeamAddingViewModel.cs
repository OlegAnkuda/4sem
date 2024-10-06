using _253503_Ankuda.Applicationn.TeamUseCases.Commands.AddTeam;
using _253503_Ankuda.Applicationn.TeamUseCases.Queries.GetAllTeams;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _253503_Ankuda.UI.ViewModels
{
    public partial class TeamAddingViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        private string _teamName = string.Empty;
        private string _teamCreator = string.Empty;
        private string _notifyString = string.Empty;
        private string _notifyStringColor = "White";
        public string TeamName
        {
            get { return _teamName; }
            set { _teamName = value; OnPropertyChanged(); }
        }
        public string TeamCreator
        {
            get { return _teamCreator; }
            set { _teamCreator = value; OnPropertyChanged(); }
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
        public TeamAddingViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [RelayCommand]
        async Task AddButtonClicked() => await AddTeam();

        public async Task AddTeam()
        {
            if (TeamName == string.Empty) { NotifyString = "Name is Empty"; NotifyStringColor = "Red"; return; }
            if (TeamCreator == string.Empty) { NotifyString = "Creator is Empty"; NotifyStringColor = "Red"; return; }
            await _mediator.Send(new AddTeamRequest(TeamName, TeamCreator));
            NotifyString = "Team added successfully!";
            NotifyStringColor = "Green";
            TeamName = string.Empty;
            TeamCreator = string.Empty;
        }
    }
}