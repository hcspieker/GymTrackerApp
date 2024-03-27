using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class ManageRoutineModel : ObservableObject
    {
        public readonly int Id;

        [ObservableProperty]
        private string title;

        public ObservableCollection<string> WorkoutSummaries { get; set; }

        public ManageRoutineModel(PlannedRoutine plannedRoutine)
        {
            Id = plannedRoutine.Id;
            Title = plannedRoutine.Title;

            var summaries = plannedRoutine.PlannedWorkouts
                .Select(x => x.PlannedExercises.Aggregate(
                    $"{x.Title}: ",
                    (current, next) => $"{current}{next.Name} ({next.AmountOfWorkSets}x{next.RepsPerWorkSet}), ",
                    result => result.Substring(0, result.Length - 2)))
                .ToList();

            WorkoutSummaries = new ObservableCollection<string>(summaries);
        }
    }
}
