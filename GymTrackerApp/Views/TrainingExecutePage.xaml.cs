using GymTrackerApp.ViewModels;

namespace GymTrackerApp.Views;

public partial class TrainingExecutePage : ContentPage
{
    public TrainingExecutePage(TrainingExecuteViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}