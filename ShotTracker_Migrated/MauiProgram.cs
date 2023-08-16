using CommunityToolkit.Maui;
using Plugin.Maui.AppRating;
using ShotTracker.Services;
using ShotTracker.Views;

namespace ShotTracker;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit();
        builder.Services.AddSingleton<IDispatcherService, DispatcherService>();
        builder.Services.AddTransient<ShotEntriesPage>();
        builder.Services.AddSingleton<IAppRating>(AppRating.Default);
        return builder.Build();
    }
}
