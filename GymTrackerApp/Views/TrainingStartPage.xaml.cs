using GymTrackerApp.ViewModels;

namespace GymTrackerApp.Views;

public partial class TrainingStartPage : ContentPage
{
    public TrainingStartPage(TrainingStartViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}