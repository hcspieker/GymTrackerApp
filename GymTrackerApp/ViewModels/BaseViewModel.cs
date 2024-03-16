using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GymTrackerApp.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        protected async Task Notify(string message, ToastDuration duration = ToastDuration.Short)
        {
            try
            {
                await Toast.Make(message, duration).Show();
            }
            catch (Exception)
            {
                try
                {
                    await Shell.Current.DisplayAlert("", message, "close");
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}
