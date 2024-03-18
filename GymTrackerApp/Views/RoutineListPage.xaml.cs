using GymTrackerApp.ViewModels;

namespace GymTrackerApp.Views;

public partial class RoutineListPage : ContentPage
{
    public RoutineListPage(RoutineListViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}