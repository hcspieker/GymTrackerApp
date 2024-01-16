using GymTrackerApp.ViewModels;

namespace GymTrackerApp.Views;

public partial class StartPage : ContentPage
{
    public StartPage(StartViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}