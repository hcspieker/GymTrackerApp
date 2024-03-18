using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Data;
using GymTrackerApp.Models;
using GymTrackerApp.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace GymTrackerApp.ViewModels
{
    public partial class RoutineListViewModel : BaseViewModel
    {
        public ObservableCollection<ManageRoutineModel> Routines { get; set; }

        public RoutineListViewModel()
        {
            Routines = new();
        }

        [RelayCommand]
        async Task Appearing()
        {
            Routines.Clear();

            using var context = new GymTrackerContext();
            var entries = context.PlannedRoutines
                .Include(x => x.PlannedWorkouts)
                    .ThenInclude(x => x.PlannedExercises)
                .Select(x => new ManageRoutineModel(x))
                .ToList();

            if (!entries.Any()) return;

            foreach (var entry in entries)
            {
                Routines.Add(entry);
            }

            await Notify("loaded routines");
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

            await Shell.Current.GoToAsync(nameof(RoutineCreatePage), true, new Dictionary<string, object> { { "Title", title } });
        }

        [RelayCommand]
        async Task Details(ManageRoutineModel routine)
        {
            await Shell.Current.DisplayAlert("Info", $"Clicked on details of '{routine.Title}'", "close");
        }
    }
}
