using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;

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

        public PlannedExercise ConvertToEty()
        {
            return new PlannedExercise
            {
                Name = Name,
                AmountOfWarmupSets = WarmupSets,
                AmountOfWorkSets = WorkSets,
                RepsPerWorkSet = RepsPerWorkSet
            };
        }
    }
}
