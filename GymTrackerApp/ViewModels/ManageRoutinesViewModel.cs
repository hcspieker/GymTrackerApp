using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Views;

namespace GymTrackerApp.ViewModels
{
    public partial class ManageRoutinesViewModel : BaseViewModel
    {
        [RelayCommand]
        async Task CreateNewRoutine()
        {
            await Shell.Current.DisplayAlert("Info", $"Clicked on create routine", "close");
        }

        [RelayCommand]
        async Task Details(string element)
        {
            await Shell.Current.DisplayAlert("Info", $"Clicked on details of '{element}'", "close");
        }
    }
}
