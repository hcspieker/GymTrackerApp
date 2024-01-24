using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class CreateWorkoutModel : BaseCreateModel
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
    }
}
