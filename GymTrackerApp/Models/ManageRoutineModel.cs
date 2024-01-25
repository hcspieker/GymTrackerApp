using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class ManageRoutineModel : ObservableObject
    {
        [ObservableProperty]
        private string title;

        public ObservableCollection<string> WorkoutSummaries { get; set; }

        public ManageRoutineModel() : this(string.Empty, new List<string>())
        {

        }

        public ManageRoutineModel(string title, List<string> workoutSummaries)
        {
            Title = title;
            WorkoutSummaries = new ObservableCollection<string>(workoutSummaries);
        }
    }
}
