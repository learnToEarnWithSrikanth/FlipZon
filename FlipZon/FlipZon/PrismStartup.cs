using FlipZon.ViewModels;
namespace MauiSampleTest;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
       
        builder.RegisterTypes(RegisterTypes)
              .OnAppStart("NavigationPage/HomeScreen");
      

    }
    //private static Page DetermineMainPage()
    //{
    //    if (Preferences.ContainsKey(Constants.USER_ID))
    //    {
    //        var id = Preferences.Get(Constants.USER_ID, -1);
    //        if (id != -1)
    //        {
    //            // User is logged in, navigate to HomeScreen using Prism
    //            return new NavigationPage(new HomeScreen());
    //        }
    //    }

    //    // User is not logged in or user ID is not found, navigate to LoginScreen using Prism
    //    return new NavigationPage(new LoginScreen());
    //}

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<HomeScreen, HomeScreenViewModel>();
        containerRegistry.RegisterForNavigation<ProductDetailsScreen, ProductDetailsScreenViewModel>();
        containerRegistry.RegisterForNavigation<CartScreen, CartScreenViewModel>();
        containerRegistry.RegisterForNavigation<ProductsScreen, ProductsScreenViewModel>();
        containerRegistry.RegisterForNavigation<SearchScreen, SearchScreenViewModel>();
        containerRegistry.RegisterForNavigation<LoginScreen, LoginScreenViewModel>();
        containerRegistry.RegisterForNavigation<SignupScreen, SignupScreenViewModel>();
        containerRegistry.RegisterForNavigation<AddressListScreen, AddressListScreenViewModel>();
        containerRegistry.RegisterForNavigation<AddAddressScreen, AddAddressScreenViewModel>();
    }
    
}

