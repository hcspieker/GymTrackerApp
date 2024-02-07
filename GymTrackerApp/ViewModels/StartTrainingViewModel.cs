using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Views;

namespace GymTrackerApp.ViewModels
{
    public partial class StartTrainingViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool usePlan;

        public StartTrainingViewModel()
        {
            UsePlan = true;
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

            await Shell.Current.GoToAsync(nameof(RunTrainingPage));
        }
    }
}
