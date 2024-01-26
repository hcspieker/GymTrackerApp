using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Models;
using System.Collections.ObjectModel;

namespace GymTrackerApp.ViewModels
{
    public partial class StartViewModel : BaseViewModel
    {
        public ObservableCollection<WorkoutSummaryModel> Workouts { get; set; }

        public StartViewModel()
        {
            Workouts = new ObservableCollection<WorkoutSummaryModel>();

            for (int i = 0; i < 10; i++)
            {
                Workouts.Add(new WorkoutSummaryModel
                {
                    RoutineTitle = "Routine A",
                    WorkoutTitle = $"Workout {(i % 2 == 0 ? "1" : "2")}",
                    Start = DateTime.Now.AddDays(-(i + 1)),
                    End = DateTime.Now.AddDays(-(i + 1)).AddHours(1),
                    ExerciseSummaries = [
                        $"Exercise 1 (5x5 1{i}0)",
                        $"Exercise 2 (3x8 {i}0)"
                    ]
                });
            }
        }

        [RelayCommand]
        async Task StartTraining()
        {
            await Shell.Current.DisplayAlert("Info", "Clicked on start training", "close");
        }

        [RelayCommand]
        async Task Details(WorkoutSummaryModel element)
        {
            await Shell.Current.DisplayAlert("Info", $"Clicked on details of {element.RoutineTitle} - " +
                $"{element.WorkoutTitle}: {element.Start:dd.MM.yyyy HH:mm:ss} - {element.End:HH:mm:ss}", "close");
        }
    }
}
