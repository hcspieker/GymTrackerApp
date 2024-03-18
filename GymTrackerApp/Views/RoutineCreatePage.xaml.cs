using GymTrackerApp.ViewModels;

namespace GymTrackerApp.Views;

public partial class RoutineCreatePage : ContentPage
{
    public RoutineCreatePage(RoutineCreateViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}