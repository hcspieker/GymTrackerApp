using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class ModifyRoutineModel : ModifyBaseModel
    {
        [ObservableProperty]
        private string title;

        public List<PickerItem<RoutineCategory>> Categories { get; set; }

        [ObservableProperty]
        private PickerItem<RoutineCategory> selectedCategory;

        public PlannedRoutineCategory PlannedCategory => ConvertCategory(SelectedCategory.Value);

        public ObservableCollection<ModifyWorkoutModel> DisplayWorkouts { get; set; }
        public List<ModifyWorkoutModel> ProcessingWorkouts { get; set; }

        public ModifyRoutineModel() : base(0)
        {
            Title = string.Empty;
            Categories = new List<PickerItem<RoutineCategory>>();
            SelectedCategory = new PickerItem<RoutineCategory>(RoutineCategory.Unknown);
            DisplayWorkouts = new();
            ProcessingWorkouts = new();
        }

        public ModifyRoutineModel(PlannedRoutine entry) : base(entry.Id)
        {
            Title = entry.Title;
            Categories = Enum.GetValues(typeof(RoutineCategory))
                .Cast<RoutineCategory>()
                .Select(x => new PickerItem<RoutineCategory>(x))
                .OrderBy(x => (int)x.Value)
                .ToList();

            SelectedCategory = Categories.Single(x => ConvertCategory(x.Value) == entry.Category);

            ProcessingWorkouts = entry.PlannedWorkouts.Select(x => new ModifyWorkoutModel(x)).ToList();
            DisplayWorkouts = new ObservableCollection<ModifyWorkoutModel>(ProcessingWorkouts);

            ModelState = ModelState.Unchanged;
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
