using GymTrackerApp.ViewModels;

namespace GymTrackerApp.Views;

public partial class StartTrainingPage : ContentPage
{
    public StartTrainingPage(StartTrainingViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}