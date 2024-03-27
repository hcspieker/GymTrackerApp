using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class ModifyWorkoutModel : ModifyBaseModel
    {
        [ObservableProperty]
        private string title;

        public ObservableCollection<ModifyExerciseModel> DisplayExercises { get; set; }
        public List<ModifyExerciseModel> ProcessingExercises { get; set; }

        public ModifyWorkoutModel(string title) : base(0)
        {
            Title = title;
            DisplayExercises = new();
            ProcessingExercises = new();
            ModelState = ModelState.Added;
        }

        public ModifyWorkoutModel(PlannedWorkout entry) : base(entry.Id)
        {
            Title = entry.Title;
            ProcessingExercises = entry.PlannedExercises.Select(x => new ModifyExerciseModel(x)).ToList();
            DisplayExercises = new ObservableCollection<ModifyExerciseModel>(ProcessingExercises);
            ModelState = ModelState.Unchanged;
        }

        public PlannedWorkout ToEty(int id)
        {
            return new PlannedWorkout
            {
                PlannedRoutineId = id,
                Title = Title,
                PlannedExercises = ProcessingExercises.Select(x => x.ToEty()).ToList()
            };
        }
    }
}
