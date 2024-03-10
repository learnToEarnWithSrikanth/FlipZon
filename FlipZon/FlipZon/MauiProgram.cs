using CarouselView;
using CommunityToolkit.Maui;
using Controls.UserDialogs.Maui;
using FlipZon.CustomRenders;
#if ANDROID
using FlipZon.Platforms.Android.Handlers;
using Mopups.Hosting;
using Mopups.Interfaces;
using Mopups.Services;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
#endif
#if IOS
using FlipZon.Platforms.iOS.Handlers;
#endif
namespace MauiSampleTest;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UsePrismApp<App>(PrismStartup.Configure)
            .UseMauiCommunityToolkit()
            .UseMauiCarouselView()
            .UseUserDialogs(() =>
            {
                 ToastConfig.DefaultCornerRadius = 15;
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-brands-400.ttf", "FAIcons");
            })
            .ConfigureMopups()
            
            .ConfigureMauiHandlers(handlers =>
            {
#if ANDROID
                    handlers.AddHandler(typeof(CustomEntry), typeof(CustomEntryHandler));
#endif
#if IOS
                    handlers.AddHandler(typeof(CustomEntry), typeof(CustomEntryHandler));
#endif
            })
           .RegisterAppServices();


        return builder.Build();
    }
    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        //Add framework services
        mauiAppBuilder.Services.AddSingleton<IDataService, DataService>();
        mauiAppBuilder.Services.AddSingleton<IRestService, RestService>();
        mauiAppBuilder.Services.AddSingleton<IDataBase, DataBase>();
        mauiAppBuilder.Services.AddSingleton<IPopupNavigation>(MopupService.Instance);
        mauiAppBuilder.Services.AddSingleton(typeof(IFingerprint), CrossFingerprint.Current);
        return mauiAppBuilder;
    }
    
}

