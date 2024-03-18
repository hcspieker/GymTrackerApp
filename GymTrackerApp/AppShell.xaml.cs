using GymTrackerApp.Views;

namespace GymTrackerApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RoutineCreatePage), typeof(RoutineCreatePage));
            Routing.RegisterRoute(nameof(TrainingStartPage), typeof(TrainingStartPage));
            Routing.RegisterRoute(nameof(TrainingExecutePage), typeof(TrainingExecutePage));
        }
    }
}
