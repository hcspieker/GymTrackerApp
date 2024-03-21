using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class ExecuteWorkoutModel : BaseModel
    {
        private int? PlannedWorkoutId;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Duration))]
        private DateTime? startTime, endTime;

        public string Duration => CalculateDuration();

        private string CalculateDuration()
        {
            if (!StartTime.HasValue || !EndTime.HasValue)
                return "unknown";

            var duration = EndTime.Value - StartTime.Value;

            return $"{duration.Hours}h {duration.Minutes}m {duration.Seconds}s";
        }

        public ObservableCollection<ExecuteExerciseModel> Exercises { get; } = new();

        public ExecuteWorkoutModel()
        {
            Title = string.Empty;
        }

        public ExecuteWorkoutModel(Workout entry)
        {
            Title = entry.Title;
            StartTime = entry.StartTime;
            EndTime = entry.EndTime;
            Exercises = new ObservableCollection<ExecuteExerciseModel>(entry.Exercises
                .Select(x => new ExecuteExerciseModel(x)));
        }

        public ExecuteWorkoutModel(PlannedWorkout workout)
        {
            PlannedWorkoutId = workout.Id;
            Title = workout.Title;
            Exercises = new ObservableCollection<ExecuteExerciseModel>(
                workout.PlannedExercises.Select(x => new ExecuteExerciseModel(x)));
        }

        public Workout ConvertToEty()
        {
            return new Workout
            {
                PlannedWorkoutId = PlannedWorkoutId,
                Title = Title,
                StartTime = StartTime,
                EndTime = EndTime,
                Exercises = Exercises.Select(x => x.ConvertToEty()).ToList()
            };
        }

        public void RemoveSet(ExecuteSetModel set)
        {
            foreach (var exercise in Exercises)
            {
                if (exercise.WarmupSets.Any(x => x.TemporaryId == set.TemporaryId))
                {
                    exercise.WarmupSets.Remove(set);
                    return;
                }
                if (exercise.WorkSets.Any(x => x.TemporaryId == set.TemporaryId))
                {
                    exercise.WorkSets.Remove(set);
                    return;
                }
            }
            throw new InvalidOperationException("there is no matching set");
        }
    }
}
