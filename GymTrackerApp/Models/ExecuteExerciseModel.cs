using CommunityToolkit.Mvvm.ComponentModel;
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
    }
}
