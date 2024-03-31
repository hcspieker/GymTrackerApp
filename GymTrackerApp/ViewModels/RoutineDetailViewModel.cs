using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Data;
using GymTrackerApp.Data.Entity;
using GymTrackerApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace GymTrackerApp.ViewModels
{
    public partial class RoutineDetailViewModel : BaseViewModel, IQueryAttributable
    {
        private int Id;

        [ObservableProperty]
        private ModifyRoutineModel routine;

        public RoutineDetailViewModel()
        {
            Routine = new ModifyRoutineModel();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("id"))
            {
                Id = Convert.ToInt32(HttpUtility.UrlDecode(query["id"].ToString() ?? "0"));
            }
        }

        [RelayCommand]
        async Task Appearing()
        {
            if (Id == 0)
            {
                await Notify("routine id is missing");
                await Shell.Current.GoToAsync("..");
                return;
            }

            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                using var context = new GymTrackerContext();
                var entry = context.PlannedRoutines
                    .Include(x => x.PlannedWorkouts)
                        .ThenInclude(x => x.PlannedExercises)
                    .Include(x => x.PlannedWorkouts)
                        .ThenInclude(x => x.Workouts)
                    .Single(x => x.Id == Id);

                Routine = new ModifyRoutineModel(entry);
                Routine.SelectedCategory = Routine.Categories.Single(x => (int)x.Value == (int)entry.Category);
                await Notify("loaded routine");
            }
            catch (Exception)
            {
                await Notify($"error while loading routine {Id}");
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        async Task EditRoutineTitle()
        {
            var result = await Shell.Current.DisplayPromptAsync("Rename",
                $"Change routine title", initialValue: Routine.Title, keyboard: Keyboard.Text);

            if (result == Routine.Title || string.IsNullOrWhiteSpace(result))
                return;

            Routine.Title = result.Trim();
            Routine.ModelState = ModelState.Modified;
        }

        [RelayCommand]
        async Task AddWorkout()
        {
            var result = await Shell.Current.DisplayPromptAsync("Create",
                $"Add a Workout", keyboard: Keyboard.Text);

            if (string.IsNullOrWhiteSpace(result)) return;

            var workout = new ModifyWorkoutModel(result.Trim());
            Routine.ProcessingWorkouts.Add(workout);
            Routine.DisplayWorkouts.Add(workout);
        }

        [RelayCommand]
        async Task EditWorkoutTitle(ModifyWorkoutModel workout)
        {
            var result = await Shell.Current.DisplayPromptAsync("Rename",
                $"Change workout title", initialValue: workout.Title, keyboard: Keyboard.Text);

            if (result == workout.Title || string.IsNullOrWhiteSpace(result))
                return;

            workout.Title = result.Trim();

            workout.ModelState = ModelState.Modified;
        }

        [RelayCommand]
        void DeleteWorkout(ModifyWorkoutModel workout)
        {
            workout.ModelState = ModelState.Deleted;
            Routine.DisplayWorkouts.Remove(workout);

            if (workout.Id == 0)
                Routine.ProcessingWorkouts.Remove(workout);
        }

        [RelayCommand]
        async Task AddExercise(ModifyWorkoutModel workout)
        {
            var result = await Shell.Current.DisplayPromptAsync("Add Exercise",
                $"Which exercise do you want to add?", keyboard: Keyboard.Text);

            if (string.IsNullOrWhiteSpace(result)) return;

            var exercise = new ModifyExerciseModel(result.Trim());
            workout.DisplayExercises.Add(exercise);
            workout.ProcessingExercises.Add(exercise);
        }

        [RelayCommand]
        void DeleteExercise(ModifyExerciseModel exercise)
        {
            exercise.ModelState = ModelState.Deleted;
            var workout = Routine.ProcessingWorkouts.Single(x => x.ProcessingExercises.Any(y => y == exercise));
            workout.DisplayExercises.Remove(exercise);

            if (exercise.Id == 0)
                workout.ProcessingExercises.Remove(exercise);
        }

        [RelayCommand]
        async Task Delete()
        {
            using var context = new GymTrackerContext();
            var workouts = context.Workouts
                .ToList();

            var workoutsToChange = workouts.Where(x => Routine.ProcessingWorkouts
                .Any(y => y.Id == x.PlannedWorkoutId))
                .ToList();

            foreach (var workoutToChange in workoutsToChange)
            {
                workoutToChange.PlannedWorkoutId = null;
            }
            var entry = context.PlannedRoutines
                .Include(x => x.PlannedWorkouts)
                .ThenInclude(x => x.PlannedExercises)
                .Single(x => x.Id == Routine.Id);

            context.Remove(entry);
            context.SaveChanges();

            await Notify("deleted routine");
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task Reset()
        {
            await LoadData();
        }

        #region Save Logic

        [RelayCommand]
        async Task SaveChanges()
        {
            using var context = new GymTrackerContext();
            var entry = context.PlannedRoutines
                .Include(x => x.PlannedWorkouts)
                    .ThenInclude(x => x.PlannedExercises)
                .Include(x => x.PlannedWorkouts)
                    .ThenInclude(x => x.Workouts)
                .Single(x => x.Id == Id);

            ModifyStoredRoutine(entry);
            HandleWorkoutChanges(entry, context);

            await context.SaveChangesAsync();

            await Notify("saved routine");
            await Shell.Current.GoToAsync("..");
        }

        private void ModifyStoredRoutine(PlannedRoutine plannedRoutine)
        {
            plannedRoutine.Title = Routine.Title;
            plannedRoutine.Category = Routine.PlannedCategory;
        }

        private void HandleWorkoutChanges(PlannedRoutine plannedRoutine, GymTrackerContext context)
        {
            foreach (var workout in Routine.ProcessingWorkouts)
            {
                if (workout.Id != 0 && workout.ModelState == ModelState.Deleted)
                {
                    var plannedWorkout = plannedRoutine.PlannedWorkouts.Single(x => x.Id == workout.Id);
                    foreach (var training in plannedWorkout.Workouts)
                    {
                        training.PlannedWorkoutId = null;
                    }
                    context.Remove(plannedWorkout);
                }
                else if (workout.ModelState == ModelState.Added)
                {
                    context.Add(workout.ToEty(Routine.Id));
                }
                else if (workout.Id != 0)
                {
                    var storedWorkout = plannedRoutine.PlannedWorkouts.Single(x => x.Id == workout.Id);

                    if (storedWorkout.Title != workout.Title)
                        storedWorkout.Title = workout.Title;

                    HandleExerciseChanges(workout, storedWorkout.PlannedExercises, context);
                }
            }
        }

        private void HandleExerciseChanges(ModifyWorkoutModel worrkout, List<PlannedExercise> plannedExercises,
            GymTrackerContext context)
        {
            foreach (var exercise in worrkout.ProcessingExercises)
            {
                if (exercise.Id == 0 && exercise.ModelState == ModelState.Deleted)
                    continue;

                switch (exercise.ModelState)
                {
                    case ModelState.Added:
                        context.Add(exercise.ToEty(worrkout.Id));
                        break;

                    case ModelState.Deleted:
                        context.Remove(plannedExercises.Single(x => x.Id == exercise.Id));
                        break;

                    case ModelState.Modified:
                        var storedExercise = plannedExercises.Single(x => x.Id == exercise.Id);
                        storedExercise.AmountOfWarmupSets = exercise.WarmupSets;
                        storedExercise.AmountOfWorkSets = exercise.WorkSets;
                        storedExercise.RepsPerWorkSet = exercise.RepsPerWorkSet;
                        break;
                }
            }
        }

        #endregion
    }
}
