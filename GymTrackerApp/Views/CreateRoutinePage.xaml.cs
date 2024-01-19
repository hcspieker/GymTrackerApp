using GymTrackerApp.ViewModels;

namespace GymTrackerApp.Views;

public partial class CreateRoutinePage : ContentPage
{
    public CreateRoutinePage(CreateRoutineViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}