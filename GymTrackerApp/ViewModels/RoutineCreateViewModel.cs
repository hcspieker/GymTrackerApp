﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Data;
using GymTrackerApp.Models;

namespace GymTrackerApp.ViewModels
{
    [QueryProperty(nameof(RoutineTitle), "Title")]
    public partial class RoutineCreateViewModel : BaseViewModel
    {
        public string RoutineTitle
        {
            get { return Routine.Title; }
            set { Routine.Title = value; }
        }

        [ObservableProperty]
        private CreateRoutineModel routine;

        public RoutineCreateViewModel()
        {
            Routine = new();
        }

        [RelayCommand]
        async Task EditRoutineTitle()
        {
            var result = await Shell.Current.DisplayPromptAsync("Rename",
                $"Change routine title", initialValue: Routine.Title, keyboard: Keyboard.Text);

            if (result == Routine.Title || string.IsNullOrWhiteSpace(result))
                return;

            Routine.Title = result.Trim();
        }

        [RelayCommand]
        async Task AddWorkout()
        {
            var result = await Shell.Current.DisplayPromptAsync("Create",
                $"Add a Workout", keyboard: Keyboard.Text);

            if (string.IsNullOrWhiteSpace(result)) return;

            Routine.Workouts.Add(new(result.Trim()));
        }

        [RelayCommand]
        async Task EditWorkoutTitle(CreateWorkoutModel workout)
        {
            var result = await Shell.Current.DisplayPromptAsync("Rename",
                $"Change workout title", initialValue: workout.Title, keyboard: Keyboard.Text);

            if (result == workout.Title || string.IsNullOrWhiteSpace(result))
                return;

            workout.Title = result.Trim();
        }

        [RelayCommand]
        async Task DeleteWorkout(CreateWorkoutModel workout)
        {
            var delete = await Shell.Current.DisplayAlert("Warning",
                $"Do you really want to delete the workout {workout.Title}?", "yes", "no");

            if (!delete)
                return;

            Routine.Workouts.Remove(workout);
        }

        [RelayCommand]
        async Task AddExercise(CreateWorkoutModel workout)
        {
            var result = await Shell.Current.DisplayPromptAsync("Add Exercise",
                $"Which exercise do you want to add?", keyboard: Keyboard.Text);

            if (string.IsNullOrWhiteSpace(result)) return;

            workout.Exercises.Add(new CreateExerciseModel(result.Trim()));
        }

        [RelayCommand]
        async Task DeleteExercise(CreateExerciseModel exercise)
        {
            var delete = await Shell.Current.DisplayAlert("Warning",
                $"Do you really want to delete the exercise {exercise.Name}?", "yes", "no");

            if (!delete)
                return;

            var workout = Routine.Workouts.Single(x => x.Exercises.Any(y => y.TemporaryId == exercise.TemporaryId));
            workout.Exercises.Remove(exercise);
        }

        [RelayCommand]
        async Task Save()
        {
            if (Routine.SelectedCategory == null)
            {
                await Shell.Current.DisplayAlert("Warning", "Category is mandatory", "close");
                return;
            }

            var plannedRoutine = Routine.ConvertToEty();

            using var context = new GymTrackerContext();
            context.Add(plannedRoutine);
            await context.SaveChangesAsync();

            await Notify("created new routine");
            await Shell.Current.GoToAsync("..");
        }
    }
}
