using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Views;

namespace GymTrackerApp.ViewModels
{
    public partial class ManageRoutinesViewModel : BaseViewModel
    {
        [RelayCommand]
        async Task CreateNewRoutine()
        {
            await Shell.Current.GoToAsync(nameof(CreateRoutinePage));
        }

        [RelayCommand]
        async Task Details(string element)
        {
            await Shell.Current.DisplayAlert("Info", $"Clicked on details of '{element}'", "close");
        }
    }
}
