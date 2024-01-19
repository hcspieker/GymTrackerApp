using GymTrackerApp.Views;

namespace GymTrackerApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CreateRoutinePage), typeof(CreateRoutinePage));
        }
    }
}
