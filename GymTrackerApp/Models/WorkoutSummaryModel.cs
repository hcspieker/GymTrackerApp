using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class WorkoutSummaryModel : ObservableObject
    {
        public int Id { get; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UsingRoutine))]
        [NotifyPropertyChangedFor(nameof(NotUsingRoutine))]
        private string routineTitle;

        [ObservableProperty]
        private string workoutTitle;

        public bool UsingRoutine => !string.IsNullOrWhiteSpace(RoutineTitle);
        public bool NotUsingRoutine => !UsingRoutine;

        [ObservableProperty]
        private DateTime? start, end;

        public ObservableCollection<string> ExerciseSummaries { get; set; }

        public WorkoutSummaryModel(Workout workout)
        {
            Id = workout.Id;
            RoutineTitle = workout.PlannedWorkout?.PlannedRoutine?.Title ?? string.Empty;
            WorkoutTitle = workout.Title;
            Start = workout.StartTime;
            End = workout.EndTime;

            ExerciseSummaries = new ObservableCollection<string>(workout.Exercises.Select(CreateSetSummary));
        }

        private string CreateSetSummary(Exercise exercise)
        {
            var lastSet = exercise.ExerciseSets
                .Where(x => x.SetType == ExerciseSetType.Work)
                .OrderBy(x => x.Id)
                .LastOrDefault();

            if (lastSet == null)
                return exercise.Name;

            if (lastSet.Weight == null)
                return $"{exercise.Name} (final set: {lastSet.Repetitions})";

            return $"{exercise.Name} (final set: {lastSet.Repetitions}x{lastSet.Weight})";
        }
    }
}
