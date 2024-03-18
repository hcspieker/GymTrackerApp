using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class ExecuteExerciseModel : BaseModel
    {
        [ObservableProperty]
        private string name;

        public ObservableCollection<ExecuteSetModel> WarmupSets { get; } = new();
        public ObservableCollection<ExecuteSetModel> WorkSets { get; } = new();

        public ExecuteExerciseModel() : this(string.Empty)
        {
        }

        public ExecuteExerciseModel(string name)
        {
            Name = name;
        }

        public ExecuteExerciseModel(Exercise entry)
        {
            Name = entry.Name;
            WarmupSets = new ObservableCollection<ExecuteSetModel>(entry.ExerciseSets
                .Where(x => x.SetType == ExerciseSetType.Warmup)
                .Select(x => new ExecuteSetModel(x)));
            WorkSets = new ObservableCollection<ExecuteSetModel>(entry.ExerciseSets
                .Where(x => x.SetType == ExerciseSetType.Work)
                .Select(x => new ExecuteSetModel(x)));
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
