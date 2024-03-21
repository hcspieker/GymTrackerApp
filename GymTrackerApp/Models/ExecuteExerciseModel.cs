using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class ExecuteExerciseModel : BaseModel
    {
        private int? PlannedExerciseId;

        [ObservableProperty]
        private string name, warmupSetLabel, workSetLabel;

        public ObservableCollection<ExecuteSetModel> WarmupSets { get; } = new();
        public ObservableCollection<ExecuteSetModel> WorkSets { get; } = new();

        public ExecuteExerciseModel() : this(string.Empty)
        {
        }

        public ExecuteExerciseModel(string name)
        {
            Name = name;

            WarmupSetLabel = "Warm-up";
            WorkSetLabel = "Work";
        }

        public ExecuteExerciseModel(Exercise entry) : this(entry.Name)
        {
            WarmupSets = new ObservableCollection<ExecuteSetModel>(entry.ExerciseSets
                .Where(x => x.SetType == ExerciseSetType.Warmup)
                .Select(x => new ExecuteSetModel(x)));
            WorkSets = new ObservableCollection<ExecuteSetModel>(entry.ExerciseSets
                .Where(x => x.SetType == ExerciseSetType.Work)
                .Select(x => new ExecuteSetModel(x)));
        }

        public ExecuteExerciseModel(PlannedExercise exercise)
        {
            PlannedExerciseId = exercise.Id;
            Name = exercise.Name;

            WarmupSetLabel = $"Warm-up ({exercise.AmountOfWarmupSets} sets)";
            WorkSetLabel = $"Work ({exercise.AmountOfWorkSets}x{exercise.RepsPerWorkSet})";

            WarmupSets = new();
            WorkSets = new();
        }

        public Exercise ConvertToEty()
        {
            var result = new Exercise { Name = Name };

            result.ExerciseSets.AddRange(WarmupSets.Select(x => x.ConvertToEty(ExerciseSetType.Warmup)));
            result.ExerciseSets.AddRange(WorkSets.Select(x => x.ConvertToEty(ExerciseSetType.Work)));

            return result;
        }
    }
}
