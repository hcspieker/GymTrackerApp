using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class CreateRoutineModel : BaseModel
    {
        [ObservableProperty]
        private string title;

        public List<PickerItem<RoutineCategory>> Categories { get; set; }

        [ObservableProperty]
        private PickerItem<RoutineCategory> selectedCategory;

        public ObservableCollection<CreateWorkoutModel> Workouts { get; set; }

        public CreateRoutineModel()
        {
            Title = string.Empty;
            Workouts = new();
            Categories = Enum.GetValues(typeof(RoutineCategory))
                .Cast<RoutineCategory>()
                .Select(x => new PickerItem<RoutineCategory>(x))
                .OrderBy(x => (int)x.Value)
                .ToList();

            SelectedCategory = Categories.First();
        }

        public PlannedRoutine ConvertToEty()
        {
            return new PlannedRoutine
            {
                Title = Title,
                Category = ConvertCategory(SelectedCategory.Value),
                PlannedWorkouts = Workouts.Select(x => x.ConvertToEty()).ToList()
            };
        }

        private PlannedRoutineCategory ConvertCategory(RoutineCategory value) => value switch
        {
            RoutineCategory.Endurance => PlannedRoutineCategory.Endurance,
            RoutineCategory.Hypertrophy => PlannedRoutineCategory.Hypertrophy,
            RoutineCategory.Strength => PlannedRoutineCategory.Strength,
            _ => PlannedRoutineCategory.Unknown,
        };
    }
}
