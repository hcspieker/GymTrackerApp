using GymTrackerApp.ViewModels;

namespace GymTrackerApp.Views;

public partial class ManageRoutinesPage : ContentPage
{
    public ManageRoutinesPage(ManageRoutinesViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}