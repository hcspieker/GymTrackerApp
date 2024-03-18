using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Data;
using GymTrackerApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace GymTrackerApp.ViewModels
{
    public partial class TrainingDetailViewModel : BaseViewModel, IQueryAttributable
    {
        private int Id;

        [ObservableProperty]
        private ExecuteWorkoutModel workout;

        public TrainingDetailViewModel()
        {
            Workout = new ExecuteWorkoutModel();
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
                await Notify("workout id is missing");
                await Shell.Current.GoToAsync("..");
                return;
            }

            try
            {
                using var context = new GymTrackerContext();
                var entry = context.Workouts
                    .Include(x => x.Exercises)
                        .ThenInclude(x => x.ExerciseSets)
                    .Include(x => x.PlannedWorkout)
                        .ThenInclude(x => x.PlannedRoutine)
                    .Single(x => x.Id == Id);

                Workout = new ExecuteWorkoutModel(entry);
                await Notify("loaded workouts");
            }
            catch (Exception)
            {
                await Notify($"error while loading workout {Id}");
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
