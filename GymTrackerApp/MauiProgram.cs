using GymTrackerApp.ViewModels;
using GymTrackerApp.Views;
using Microsoft.Extensions.Logging;

namespace GymTrackerApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FAS");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<StartViewModel>();
            builder.Services.AddSingleton<ManageRoutinesViewModel>();
            builder.Services.AddTransient<CreateRoutineViewModel>();

            builder.Services.AddSingleton<StartPage>();
            builder.Services.AddSingleton<ManageRoutinesPage>();
            builder.Services.AddTransient<CreateRoutinePage>();

            return builder.Build();
        }
    }
}
