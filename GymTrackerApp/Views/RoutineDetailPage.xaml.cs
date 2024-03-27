using GymTrackerApp.ViewModels;

namespace GymTrackerApp.Views;

public partial class RoutineDetailPage : ContentPage
{
	public RoutineDetailPage(RoutineDetailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}