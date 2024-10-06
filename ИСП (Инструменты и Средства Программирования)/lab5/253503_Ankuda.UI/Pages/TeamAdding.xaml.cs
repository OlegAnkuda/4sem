using _253503_Ankuda.UI.ViewModels;

namespace _253503_Ankuda.UI.Pages;

public partial class TeamAdding : ContentPage
{
    public TeamAdding(TeamAddingViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}