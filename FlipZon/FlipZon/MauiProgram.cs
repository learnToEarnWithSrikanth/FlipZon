using Controls.UserDialogs.Maui;
using FlipZon.CustomRenders;
#if ANDROID
using FlipZon.Platforms.Android.Handlers;
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
            .UseUserDialogs(true,() =>
            {

            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-brands-400.ttf", "FAIcons");
            })
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
        return mauiAppBuilder;
    }
    
}

