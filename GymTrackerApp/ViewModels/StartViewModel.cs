using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Data;
using GymTrackerApp.Models;
using GymTrackerApp.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace GymTrackerApp.ViewModels
{
    public partial class StartViewModel : BaseViewModel
    {
        public ObservableCollection<WorkoutSummaryModel> Workouts { get; set; }

        public StartViewModel()
        {
            Workouts = new();
        }

        [RelayCommand]
        void Appearing()
        {
            Workouts.Clear();

            using var context = new GymTrackerContext();
            var entries = context.Workouts
                .Include(x => x.Exercises)
                    .ThenInclude(x => x.ExerciseSets)
                .Include(x => x.PlannedWorkout)
                    .ThenInclude(x => x.PlannedRoutine)
                .OrderByDescending(x => x.EndTime ?? DateTime.MinValue)
                .Select(x => new WorkoutSummaryModel(x))
                .ToList();

            if (!entries.Any()) return;

            foreach (var entry in entries)
            {
                Workouts.Add(entry);
            }
        }

        [RelayCommand]
        async Task StartTraining()
        {
            await Shell.Current.GoToAsync(nameof(StartTrainingPage));
        }

        [RelayCommand]
        async Task Details(WorkoutSummaryModel element)
        {
            var displayName = !string.IsNullOrWhiteSpace(element.RoutineTitle) ?
                $"{element.RoutineTitle} - {element.WorkoutTitle}" :
                element.WorkoutTitle;

            var message = $"Clicked on details of {displayName}: " +
                $"{element.Start:dd.MM.yyyy HH:mm} - {element.End:HH:mm}";

            await Toast.Make(message).Show();
        }
    }
}
