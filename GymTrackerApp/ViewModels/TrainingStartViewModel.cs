using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Data;
using GymTrackerApp.Data.Entity;
using GymTrackerApp.Models;
using GymTrackerApp.Views;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace GymTrackerApp.ViewModels
{
    public partial class TrainingStartViewModel : BaseViewModel
    {
        [ObservableProperty, NotifyPropertyChangedFor(nameof(NotUsePlan)),
            NotifyPropertyChangedFor(nameof(StartIsVisible))]
        private bool usePlan;

        public bool NotUsePlan => !UsePlan;

        public bool StartIsVisible => UsePlan || !string.IsNullOrWhiteSpace(WorkoutTitle);

        #region using a plan

        private List<PlannedRoutine> PlannedRoutines;

        public ObservableCollection<DropDownElement> Routines { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ChosenRoutine))]
        [NotifyPropertyChangedFor(nameof(Workouts))]
        private DropDownElement? selectedRoutine;

        public bool ChosenRoutine => SelectedRoutine != null;

        public List<DropDownElement> Workouts => PlannedRoutines.SelectMany(x => x.PlannedWorkouts)
            .Where(x => SelectedRoutine != null && x.PlannedRoutineId == SelectedRoutine.Id)
            .Select(x => new DropDownElement(x))
            .ToList();

        [ObservableProperty]
        private DropDownElement? selectedWorkout;

        #endregion

        #region not using a plan

        [ObservableProperty, NotifyPropertyChangedFor(nameof(StartIsVisible))]
        private string workoutTitle;

        #endregion

        public TrainingStartViewModel()
        {
            UsePlan = true;
            WorkoutTitle = string.Empty;
            Routines = new();
            PlannedRoutines = new();
        }

        [RelayCommand]
        async Task Appearing()
        {
            Routines.Clear();

            using var context = new GymTrackerContext();
            PlannedRoutines = context.PlannedRoutines
                .Include(x => x.PlannedWorkouts)
                .ToList();

            var entries = PlannedRoutines.Select(x => new DropDownElement(x));

            if (!entries.Any()) return;

            foreach (var entry in entries)
            {
                Routines.Add(entry);
            }

            await Notify("loaded routines");
        }

        [RelayCommand]
        async Task Start()
        {
            if (UsePlan)
            {
                if (SelectedRoutine == null)
                {
                    await Shell.Current.DisplayAlert("Warning", "Choosing a routine is mandatory", "close");
                    return;
                }
                if (SelectedWorkout == null)
                {
                    await Shell.Current.DisplayAlert("Warning", "Choosing a workout is mandatory", "close");
                    return;
                }
                await Shell.Current.GoToAsync(nameof(TrainingExecutePage), new Dictionary<string, object> { { "id", SelectedWorkout.Id } });
            }
            else
            {
                if (string.IsNullOrWhiteSpace(WorkoutTitle))
                {
                    await Shell.Current.DisplayAlert("Warning", "Workout title is mandatory", "close");
                    return;
                }

                await Shell.Current.GoToAsync(nameof(TrainingExecutePage), new Dictionary<string, object> { { "title", WorkoutTitle } });
            }
        }
    }
}
