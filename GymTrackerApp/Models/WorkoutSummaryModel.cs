using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class WorkoutSummaryModel : ObservableObject
    {
        [ObservableProperty]
        private string routineTitle, workoutTitle;

        [ObservableProperty]
        private DateTime start, end;

        public ObservableCollection<string> ExerciseSummaries { get; set; }

        public WorkoutSummaryModel() : this(string.Empty, string.Empty, DateTime.Now, DateTime.Now, new())
        {

        }

        public WorkoutSummaryModel(string routineTitle, string workoutTitle, DateTime start,
            DateTime end, List<string> exerciseSummaries)
        {
            RoutineTitle = routineTitle;
            WorkoutTitle = workoutTitle;
            Start = start;
            End = end;
            ExerciseSummaries = new ObservableCollection<string>(exerciseSummaries);
        }
    }
}
