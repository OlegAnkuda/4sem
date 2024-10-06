using _253503_Ankuda.Applicationn.CompetitorUseCases.Commands.UpdateCompetitor;
using _253503_Ankuda.Applicationn.CompetitorUseCases.Queries.GetCompetitorsByTeam;
using _253503_Ankuda.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace _253503_Ankuda.UI.ViewModels
{
    [QueryProperty(nameof(SerializedCompetitor), "competitor")]
    public partial class CompetitorDetailsViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        CompetitorEntity competitor;
        private string _newName = string.Empty;
        private string _newDescription = string.Empty;
        private string _newScore = string.Empty;
        private string _serializedCompetitor = string.Empty;
        private string _notifyNameString = string.Empty;
        private string _notifyDescriptionString = string.Empty;
        private string _notifyScoreString = string.Empty;
        private string _notifyNameStringColor = "White";
        private string _notifyDescriptionStringColor = "White";
        private string _notifyScoreStringColor = "White";
        public CompetitorDetailsViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public CompetitorEntity Competitor
        {
            get => competitor;
            set { SetProperty(ref competitor, value); OnPropertyChanged(); }
        }
        public string NewName
        {
            get { return _newName; }
            set { SetProperty(ref _newName, value); OnPropertyChanged(); }
        }
        public string NewDescription
        {
            get { return _newDescription; }
            set { SetProperty(ref _newDescription, value); OnPropertyChanged(); }
        }
        public string NewScore
        {
            get { return _newScore; }
            set { SetProperty(ref _newScore, value); OnPropertyChanged(); }
        }
        public string NotifyNameString
        {
            get { return _notifyNameString; }
            set { SetProperty(ref _notifyNameString, value); OnPropertyChanged(); }
        }
        public string NotifyDescriptionString
        {
            get { return _notifyDescriptionString; }
            set { SetProperty(ref _notifyDescriptionString, value); OnPropertyChanged(); }
        }
        public string NotifyScoreString
        {
            get { return _notifyScoreString; }
            set { SetProperty(ref _notifyScoreString, value); OnPropertyChanged(); }
        }
        public string NotifyNameStringColor
        {
            get { return _notifyNameStringColor; }
            set { SetProperty(ref _notifyNameStringColor, value); OnPropertyChanged(); }
        }
        public string NotifyDescriptionStringColor
        {
            get { return _notifyDescriptionStringColor; }
            set { SetProperty(ref _notifyDescriptionStringColor, value); OnPropertyChanged(); }
        }
        public string NotifyScoreStringColor
        {
            get { return _notifyScoreStringColor; }
            set { SetProperty(ref _notifyScoreStringColor, value); OnPropertyChanged(); }
        }
        public string SerializedCompetitor
        {
            get => _serializedCompetitor;
            set
            {
                _serializedCompetitor = value;
                competitor = JsonSerializer.Deserialize<CompetitorEntity>(Uri.UnescapeDataString(_serializedCompetitor));
            }
        }
        [RelayCommand]
        async Task ImageChangeButtonClicked() => await SelectImageAsync();
        [RelayCommand]
        async Task NameChangeButtonClicked() => await UpdateName();
        [RelayCommand]
        async Task DescriptionChangeButtonClicked() => await UpdateDescription();
        [RelayCommand]
        async Task ScoreChangeButtonClicked() => await UpdateScore();

        [RelayCommand]
        async Task UpdateCompetitorDetails() => await GetCompetitorDetails();
        public async Task GetCompetitorDetails()
        {
            Competitor = JsonSerializer.Deserialize<CompetitorEntity>(Uri.UnescapeDataString(_serializedCompetitor));
        }
        public async Task UpdateName()
        {
            if (NewName == string.Empty) { NotifyNameString = "Name is Empty!"; NotifyNameStringColor = "Red"; return; }
            Competitor.Name = NewName;
            NewName = string.Empty;
            await _mediator.Send(new UpdateCompetitorRequest(competitor));
            Competitor = Competitor;
            NotifyNameString = "Name updated successfully!"; NotifyNameStringColor = "Green";
        }
        public async Task UpdateDescription()
        {
            if (NewDescription == string.Empty) { NotifyDescriptionString = "Name is Empty!"; NotifyDescriptionStringColor = "Red"; return; }
            Competitor.Description = NewDescription;
            NewDescription = string.Empty;
            await _mediator.Send(new UpdateCompetitorRequest(competitor));
            Competitor = Competitor;
            NotifyDescriptionString = "Description updated successfully!"; NotifyDescriptionStringColor = "Green";
        }
        public async Task UpdateScore()
        {
            if (NewScore == string.Empty) { NotifyScoreString = "Score is Empty!"; NotifyScoreStringColor = "Red"; return; }
            if (!int.TryParse(NewScore, out var score) || score < 0 || score > 100) { NotifyScoreString = "Incorrect input!"; NotifyScoreStringColor = "Red"; return; }
            Competitor.Score = score;
            NewScore = string.Empty;
            await _mediator.Send(new UpdateCompetitorRequest(competitor));
            Competitor = Competitor;
            NotifyScoreString = "Score updated successfully!"; NotifyScoreStringColor = "Green";
        }
        public async Task SelectImageAsync()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Choose image"
            });

            if (result != null)
            {
                string imagesFolder = "";
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        imagesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "Images");
                        break;
                    case Device.Android:
                        imagesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Images");
                        break;
                    case Device.UWP:
                        imagesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Images");
                        break;
                }

                if (!Directory.Exists(imagesFolder))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                        Directory.CreateDirectory(imagesFolder);
                    });
                }
                var newImagePath = $"{imagesFolder}\\{Competitor.Id}.png";
                var oldImagePath = result.FullPath;
                if (File.Exists(newImagePath))
                {
                    File.Delete(newImagePath);
                }
                File.Move(oldImagePath, newImagePath);
                Competitor = Competitor;
            }
        }
    }
}