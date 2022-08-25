using SpaceScience.Interfaces;
using SpaceScience.Services;
using SpaceScience.ViewModels;
using SpaceScience.Views;

namespace SpaceScience;

public static class MauiProgram
{

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder.UsePrismApp<App>(prism =>
        {
			// configure prism
			prism.RegisterTypes(containerRegistry =>
			{
				containerRegistry.RegisterGlobalNavigationObserver();
				containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
				containerRegistry.RegisterForNavigation<MarsPage, MarsPageViewModel>();
				containerRegistry.RegisterForNavigation<ApodPage, ApodPageViewModel>();
				containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();

                containerRegistry.RegisterScoped<IDataService, DataService>();
			})
            .AddGlobalNavigationObserver(context => context.Subscribe(x =>
            {
                if (x.Type == NavigationRequestType.Navigate)
                    Console.WriteLine($"Navigation: {x.Uri}");
                else
                    Console.WriteLine($"Navigation: {x.Type}");

                var status = x.Cancelled ? "Cancelled" : x.Result.Success ? "Success" : "Failed";
                Console.WriteLine($"Result: {status}");

                if (status == "Failed" && !string.IsNullOrEmpty(x.Result?.Exception?.Message))
                    Console.Error.WriteLine(x.Result.Exception.Message);
            }))
            .OnAppStart(navigationService => navigationService.CreateBuilder()
                    .AddNavigationSegment<MainPageViewModel>()
                    .Navigate(HandleNavigationError));
        }).ConfigureFonts(fonts =>
		{
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
       

		return builder.Build();
	}

    private static void HandleNavigationError(Exception ex)
    {
        Console.WriteLine(ex);
        System.Diagnostics.Debugger.Break();
    }
}
