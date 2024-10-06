using _253503_Ankuda.UI.ViewModels;

namespace _253503_Ankuda.UI.Pages;

public partial class CompetitorAdding : ContentPage
{
    public CompetitorAdding(CompetitorAddingViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}