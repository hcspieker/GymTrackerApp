using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Models;
using GymTrackerApp.Views;
using System.Collections.ObjectModel;

namespace GymTrackerApp.ViewModels
{
    public partial class ManageRoutinesViewModel : BaseViewModel
    {
        public ObservableCollection<ManageRoutineModel> Routines { get; set; }

        public ManageRoutinesViewModel()
        {
            Routines = new ObservableCollection<ManageRoutineModel>();

            for (int i = 0; i < 15; i++)
            {
                var workoutSummaries = new List<string>();

                var amountOfWorkouts = Random.Shared.Next(1, 5);
                for (int j = 0; j < amountOfWorkouts; j++)
                {
                    var amountOfExercises = Random.Shared.Next(1, 7);
                    var workoutSummary = $"Workout {j + 1}: ";
                    for (int k = 0; k < amountOfExercises; k++)
                    {
                        workoutSummary += $"Exercise {k + 1}, ";
                    }
                    workoutSummaries.Add(workoutSummary.Substring(0, workoutSummary.Length - 2));
                }

                Routines.Add(new ManageRoutineModel($"Routine {i + 1}", workoutSummaries));
            }
        }

        [RelayCommand]
        async Task CreateNewRoutine()
        {
            var title = await Shell.Current.DisplayPromptAsync("Create new workout routine",
                $"Supply a title for the new routine", keyboard: Keyboard.Text);

            if (string.IsNullOrWhiteSpace(title))
            {
                await Shell.Current.DisplayAlert("Create new workout routine", "Canceled creation", "close");
                return;
            }

            await Shell.Current.GoToAsync(nameof(CreateRoutinePage), true, new Dictionary<string, object> { { "Title", title } });
        }

        [RelayCommand]
        async Task Details(ManageRoutineModel routine)
        {
            await Shell.Current.DisplayAlert("Info", $"Clicked on details of '{routine.Title}'", "close");
        }
    }
}
