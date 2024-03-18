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
        async Task Appearing()
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

            await Notify("loaded workouts");
        }

        [RelayCommand]
        async Task TrainingStart()
        {
            await Shell.Current.GoToAsync(nameof(TrainingStartPage));
        }

        [RelayCommand]
        async Task Delete(WorkoutSummaryModel element)
        {
            var displayName = $"'{element.WorkoutTitle}' (" +
                (!string.IsNullOrWhiteSpace(element.RoutineTitle) ?
                $"routine: '{element.RoutineTitle}', " : "") +
                $"{element.Start:dd.MM.yyyy HH:mm} - {element.End:HH:mm})";

            var continueDeleting = await Shell.Current.DisplayAlert("Confirm",
                $"Do you really want to delete workout {displayName}?",
                "yes", "no");

            if (!continueDeleting)
                return;

            using var context = new GymTrackerContext();
            var entry = context.Workouts.Single(x => x.Id == element.Id);
            context.Remove(entry);
            await context.SaveChangesAsync();

            Workouts.Remove(element);

            await Toast.Make($"deleted workout {displayName}").Show();
        }

        [RelayCommand]
        async Task Details(WorkoutSummaryModel element)
        {
            await Shell.Current.GoToAsync(nameof(TrainingDetailPage),
                new Dictionary<string, object> { { "id", element.Id.ToString() } });
        }
    }
}
