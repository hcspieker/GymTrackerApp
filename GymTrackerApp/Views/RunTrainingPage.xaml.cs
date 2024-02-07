using GymTrackerApp.ViewModels;

namespace GymTrackerApp.Views;

public partial class RunTrainingPage : ContentPage
{
	public RunTrainingPage(RunTrainingViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}