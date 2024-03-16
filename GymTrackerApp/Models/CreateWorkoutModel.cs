using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class CreateWorkoutModel : BaseModel
    {
        [ObservableProperty]
        private string title;

        public ObservableCollection<CreateExerciseModel> Exercises { get; set; }

        public CreateWorkoutModel() : this(string.Empty)
        {
        }

        public CreateWorkoutModel(string title)
        {
            Title = title;
            Exercises = new();
        }

        public PlannedWorkout ConvertToEty()
        {
            return new PlannedWorkout
            {
                Title = Title,
                PlannedExercises = Exercises.Select(x => x.ConvertToEty()).ToList()
            };
        }
    }
}
