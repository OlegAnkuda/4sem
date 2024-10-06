using _253503_Ankuda.UI.Pages;

namespace _253503_Ankuda.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(CompetitorDetails), typeof(CompetitorDetails));
            InitializeComponent();
            
        }
    }
}
