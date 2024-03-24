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
        async Task Details(WorkoutSummaryModel element)
        {
            await Shell.Current.GoToAsync(nameof(TrainingDetailPage),
                new Dictionary<string, object> { { "id", element.Id.ToString() } });
        }
    }
}
