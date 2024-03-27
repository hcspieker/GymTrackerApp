using GymTrackerApp.Views;

namespace GymTrackerApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RoutineCreatePage), typeof(RoutineCreatePage));
            Routing.RegisterRoute(nameof(RoutineDetailPage), typeof(RoutineDetailPage));
            Routing.RegisterRoute(nameof(TrainingStartPage), typeof(TrainingStartPage));
            Routing.RegisterRoute(nameof(TrainingExecutePage), typeof(TrainingExecutePage));
            Routing.RegisterRoute(nameof(TrainingDetailPage), typeof(TrainingDetailPage));
        }
    }
}
