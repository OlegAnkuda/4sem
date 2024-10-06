using _253503_Ankuda.UI.ViewModels;

namespace _253503_Ankuda.UI.Pages;
public partial class CompetitorDetails : ContentPage
{
    public CompetitorDetails(CompetitorDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}