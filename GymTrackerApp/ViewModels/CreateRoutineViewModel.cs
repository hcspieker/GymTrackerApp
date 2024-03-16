using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Data;
using GymTrackerApp.Models;

namespace GymTrackerApp.ViewModels
{
    [QueryProperty(nameof(RoutineTitle), "Title")]
    public partial class CreateRoutineViewModel : BaseViewModel
    {
        public string RoutineTitle
        {
            get { return Routine.Title; }
            set { Routine.Title = value; }
        }

        [ObservableProperty]
        private CreateRoutineModel routine;

        public CreateRoutineViewModel()
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

            Routine.Title = result;
        }

        [RelayCommand]
        async Task AddWorkout()
        {
            var result = await Shell.Current.DisplayPromptAsync("Create",
                $"Add a Workout", keyboard: Keyboard.Text);

            if (string.IsNullOrWhiteSpace(result)) return;

            Routine.Workouts.Add(new(result));
        }

        [RelayCommand]
        async Task EditWorkoutTitle(CreateWorkoutModel workout)
        {
            var result = await Shell.Current.DisplayPromptAsync("Rename",
                $"Change workout title", initialValue: workout.Title, keyboard: Keyboard.Text);

            if (result == workout.Title || string.IsNullOrWhiteSpace(result))
                return;

            workout.Title = result;
        }

        [RelayCommand]
        void DeleteWorkout(CreateWorkoutModel workout)
        {
            Routine.Workouts.Remove(workout);
        }

        [RelayCommand]
        async Task AddExercise(CreateWorkoutModel workout)
        {
            var result = await Shell.Current.DisplayPromptAsync("Add Exercise",
                $"Which exercise do you want to add?", keyboard: Keyboard.Text);

            if (string.IsNullOrWhiteSpace(result)) return;

            workout.Exercises.Add(new CreateExerciseModel(result));
        }

        [RelayCommand]
        void DeleteExercise(CreateExerciseModel exercise)
        {
            var workout = Routine.Workouts.Single(x => x.Exercises.Any(y => y.TemporaryId == exercise.TemporaryId));
            workout.Exercises.Remove(exercise);
        }

        [RelayCommand]
        async Task Save()
        {
            var plannedRoutine = Routine.ConvertToEty();

            using var context = new GymTrackerContext();
            context.Add(plannedRoutine);
            await context.SaveChangesAsync();

            await Notify("created new routine");
            await Shell.Current.GoToAsync("..");
        }
    }
}
