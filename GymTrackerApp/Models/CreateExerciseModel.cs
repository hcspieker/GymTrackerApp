using CommunityToolkit.Mvvm.ComponentModel;

namespace GymTrackerApp.Models
{
    public partial class CreateExerciseModel : BaseModel
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int warmupSets, workSets, repsPerWorkSet;

        public CreateExerciseModel() : this(string.Empty)
        {
        }

        public CreateExerciseModel(string name)
        {
            Name = name;
            WarmupSets = 2;
            WorkSets = 3;
            RepsPerWorkSet = 5;
        }
    }
}
