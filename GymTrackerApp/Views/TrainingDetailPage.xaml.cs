using GymTrackerApp.ViewModels;

namespace GymTrackerApp.Views;

public partial class TrainingDetailPage : ContentPage
{
    public TrainingDetailPage(TrainingDetailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}