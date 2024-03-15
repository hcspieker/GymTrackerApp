using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Data;
using GymTrackerApp.Models;
using System.Web;

namespace GymTrackerApp.ViewModels
{
    public partial class RunTrainingViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        private ExecuteWorkoutModel workout;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(WorkoutWaiting))]
        [NotifyPropertyChangedFor(nameof(WorkoutStarted))]
        [NotifyPropertyChangedFor(nameof(WorkoutFinished))]
        private RunWorkoutState state;

        public bool WorkoutWaiting => State == RunWorkoutState.Waiting;
        public bool WorkoutStarted => State == RunWorkoutState.Started;
        public bool WorkoutFinished => State == RunWorkoutState.Finished;

        public RunTrainingViewModel()
        {
            State = RunWorkoutState.Waiting;
            Workout = new ExecuteWorkoutModel();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("title"))
                Workout.Title = HttpUtility.UrlDecode(query["title"].ToString() ?? "");
        }

        #region flow

        [RelayCommand]
        void Start()
        {
            Workout.StartTime = DateTime.Now;
            State = RunWorkoutState.Started;
        }

        [RelayCommand]
        void Cancel()
        {
            Workout.StartTime = null;
            State = RunWorkoutState.Waiting;
        }

        [RelayCommand]
        void Finish()
        {
            Workout.EndTime = DateTime.Now;
            State = RunWorkoutState.Finished;
        }

        [RelayCommand]
        void Resume()
        {
            Workout.EndTime = null;
            State = RunWorkoutState.Started;
        }

        [RelayCommand]
        async Task SaveAndExit()
        {
            using var context = new GymTrackerContext();
            context.Add(Workout.ConvertToEty());
            await context.SaveChangesAsync();

            await Shell.Current.GoToAsync("../..");
        }

        #endregion

        #region exercise management

        [RelayCommand]
        async Task AddExercise()
        {
            var name = await Shell.Current.DisplayPromptAsync("Add exercise",
                "Supply the name of an exercise to add");

            if (string.IsNullOrWhiteSpace(name))
            {
                await Shell.Current.DisplayAlert("", "Cancelled add exercise", "close");
                return;
            }

            Workout.Exercises.Add(new ExecuteExerciseModel(name));
        }

        [RelayCommand]
        async Task DeleteExercise(ExecuteExerciseModel exercise)
        {
            var delete = await Shell.Current.DisplayAlert("Warning",
                $"Do you really want to delete the exercise {exercise.Name}?", "yes", "no");

            if (!delete)
                return;

            try
    {
                Workout.Exercises.Remove(exercise);
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert($"Delete exercise {exercise.Name}", "An error occured", "close");
            }
        }

        #endregion

        #region set management

        [RelayCommand]
        async Task AddWarmupSet(ExecuteExerciseModel exercise)
        {
            await TryAddSet("warmup", exercise.WarmupSets.Add);
        }

        [RelayCommand]
        async Task AddWorkoutSet(ExecuteExerciseModel exercise)
        {
            await TryAddSet("work", exercise.WorkSets.Add);
        }

        async Task TryAddSet(string setType, Action<ExecuteSetModel> addSetDelegate)
        {
            var input = await Shell.Current.DisplayPromptAsync($"Add {setType} set",
                "Specify a set with the pattern '{reps}x{weight}'", accept: "Add", placeholder: "5x123.45");

            if (string.IsNullOrWhiteSpace(input))
                return;

            if (input.Count(x => x == 'x') > 1)
            {
                await Shell.Current.DisplayAlert($"Add {setType} set", "Invalid input", "close");
                return;
            }

            try
            {
                addSetDelegate(new ExecuteSetModel(input));
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert($"Add {setType} set", "An error occured", "close");
            }
        }

        [RelayCommand]
        async Task DeleteWarmupSet(ExecuteSetModel set)
        {
            await TryDeleteSet("warmup", set);
        }

        [RelayCommand]
        async Task DeleteWorkoutSet(ExecuteSetModel set)
        {
            await TryDeleteSet("work", set);
        }

        async Task TryDeleteSet(string setType, ExecuteSetModel set)
        {
            var delete = await Shell.Current.DisplayAlert("Warning",
                $"Do you really want to delete the {setType} set with the value {set}?", "yes", "no");

            if (!delete)
                return;

            try
            {
                Workout.RemoveSet(set);
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert($"Delete {setType} set {set}", "An error occured", "close");
            }
        }

        #endregion
    }
}
