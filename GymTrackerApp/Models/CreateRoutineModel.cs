using CommunityToolkit.Mvvm.ComponentModel;
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
    }
}
