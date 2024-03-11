using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Views;

namespace GymTrackerApp.ViewModels
{
    public partial class StartTrainingViewModel : BaseViewModel
    {
        [ObservableProperty, NotifyPropertyChangedFor(nameof(NotUsePlan)),
            NotifyPropertyChangedFor(nameof(StartIsVisible))]
        private bool usePlan;

        public bool NotUsePlan => !UsePlan;

        public bool StartIsVisible => UsePlan || !string.IsNullOrWhiteSpace(WorkoutTitle);

        [ObservableProperty, NotifyPropertyChangedFor(nameof(StartIsVisible))]
        private string workoutTitle;

        public StartTrainingViewModel()
        {
            UsePlan = true;
            WorkoutTitle = string.Empty;
        }

        [RelayCommand]
        async Task Start()
        {
            if (UsePlan)
            {
                try
                {
                    throw new NotImplementedException();
                }
                catch (Exception)
                {
                    await Shell.Current.DisplayAlert("Info", "Using a plan isn't implemented", "close");
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(WorkoutTitle))
            {
                await Shell.Current.DisplayAlert("Warning", "Workout title is mandatory", "close");
                return;
            }

            await Shell.Current.GoToAsync(nameof(RunTrainingPage), new Dictionary<string, object> { { "title", WorkoutTitle } });
        }
    }
}
