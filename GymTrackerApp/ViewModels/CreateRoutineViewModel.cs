using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace GymTrackerApp.ViewModels
{
    public partial class CreateRoutineViewModel : BaseViewModel
    {
        public ObservableCollection<string> Workouts { get; set; }

        public CreateRoutineViewModel()
        {
            Workouts = new ObservableCollection<string>
            {
                "Workout 1",
                "Workout 2",
                "Workout 3"
            };
        }

        [RelayCommand]
        async Task EditWorkoutTitle(string oldTitle)
        {
            var result = await Shell.Current.DisplayPromptAsync("Rename",
                $"Rename Workout", initialValue: oldTitle, keyboard: Keyboard.Text);

            if (result == oldTitle || string.IsNullOrWhiteSpace(result))
                return;

            Workouts[Workouts.IndexOf(oldTitle)] = result;
        }

        [RelayCommand]
        void DeleteWorkout(string workout)
        {
            Workouts.Remove(workout);
        }

        [RelayCommand]
        async Task AddWorkout()
        {
            var result = await Shell.Current.DisplayPromptAsync("Create",
                $"Add a Workout", keyboard: Keyboard.Text);

            if (string.IsNullOrWhiteSpace(result)) return;

            Workouts.Add(result);
        }
    }
}
