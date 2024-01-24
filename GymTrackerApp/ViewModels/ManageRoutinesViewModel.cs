using CommunityToolkit.Mvvm.Input;
using GymTrackerApp.Views;

namespace GymTrackerApp.ViewModels
{
    public partial class ManageRoutinesViewModel : BaseViewModel
    {
        [RelayCommand]
        async Task CreateNewRoutine()
        {
            var title = await Shell.Current.DisplayPromptAsync("Create new workout routine",
                $"Supply a title for the new routine", keyboard: Keyboard.Text);

            if (string.IsNullOrWhiteSpace(title))
            {
                await Shell.Current.DisplayAlert("Create new workout routine", "Canceled creation", "close");
                return;
            }

            await Shell.Current.GoToAsync(nameof(CreateRoutinePage), true, new Dictionary<string, object> { { "Title", title } });
        }

        [RelayCommand]
        async Task Details(string element)
        {
            await Shell.Current.DisplayAlert("Info", $"Clicked on details of '{element}'", "close");
        }
    }
}
