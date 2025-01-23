using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Hosting;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CommunityToolkit.Maui.Sample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder()
			.UseMauiCommunityToolkit()
			.UseMauiCommunityToolkitMediaElement()

			.ConfigureMauiHandlers(handlers =>
			{
	#if IOS || MACCATALYST
				handlers.AddHandler<CollectionView, Microsoft.Maui.Controls.Handlers.Items2.CollectionViewHandler2>();
				handlers.AddHandler<CarouselView, Microsoft.Maui.Controls.Handlers.Items2.CarouselViewHandler2>();
	#endif
			})

			.UseMauiApp<App>();


		builder.Services.AddSingleton<MediaElementPage>();
		
		Routing.RegisterRoute("//ViewsGalleryPage/MediaElementPage", typeof(NavigableElement));
		builder.Services.AddTransient<NavigableElement, MediaElementViewModel>();
		
		builder.Services.AddSingleton(DeviceDisplay.Current);

#if DEBUG
		builder.Logging.AddDebug().SetMinimumLevel(LogLevel.Trace);
#endif

		return builder.Build();
	}

}