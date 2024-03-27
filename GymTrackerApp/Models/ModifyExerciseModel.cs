using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;

namespace GymTrackerApp.Models
{
    public partial class ModifyExerciseModel : ModifyBaseModel
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int warmupSets, workSets, repsPerWorkSet;

        public ModifyExerciseModel(string name) : base(0)
        {
            Name = name;
            WarmupSets = 2;
            WorkSets = 3;
            RepsPerWorkSet = 5;
            ModelState = ModelState.Added;
        }

        public ModifyExerciseModel(PlannedExercise entry) : base(entry.Id)
        {
            Name = entry.Name;
            WarmupSets = entry.AmountOfWarmupSets;
            WorkSets = entry.AmountOfWorkSets;
            RepsPerWorkSet = entry.RepsPerWorkSet;
            ModelState = ModelState.Unchanged;
        }

        public PlannedExercise ToEty(int plannedWorkoutId = 0)
        {
            return new PlannedExercise
            {
                PlannedWorkoutId = plannedWorkoutId,
                Name = Name,
                AmountOfWarmupSets = WarmupSets,
                AmountOfWorkSets = WorkSets,
                RepsPerWorkSet = RepsPerWorkSet
            };
        }
    }
}
