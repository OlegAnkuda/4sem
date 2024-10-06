using _253503_Ankuda.UI.ViewModels;

namespace _253503_Ankuda.UI.Pages;

public partial class TeamsPage : ContentPage
{
    public TeamsPage(TeamsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}