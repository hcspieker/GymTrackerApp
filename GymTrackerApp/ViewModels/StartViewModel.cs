﻿using CommunityToolkit.Mvvm.Input;

namespace GymTrackerApp.ViewModels
{
    public partial class StartViewModel : BaseViewModel
    {
        [RelayCommand]
        async Task StartTraining()
        {
            await Shell.Current.DisplayAlert("Info", "Clicked on start training", "close");
        }

        [RelayCommand]
        async Task Details(string element)
        {
            await Shell.Current.DisplayAlert("Info", $"Clicked on details of '{element}'", "close");
        }
    }
}