using FlipZon.ViewModels;
namespace MauiSampleTest;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
       
        builder.RegisterTypes(RegisterTypes)
              .OnAppStart(OnAppStart);
      

    }
    private static void OnAppStart(IContainerProvider containerProvider, INavigationService navigationService)
    {
        if (Preferences.ContainsKey(Constants.USER_ID))
        {
            var id = Preferences.Get(Constants.USER_ID, -1);
            if (id != -1)
            {
                // User is logged in, navigate to HomeScreen 
                navigationService.NavigateAsync("NavigationPage/HomeScreen");
            }
        }
        else
        {
            // User is not logged navigate to LoginScreen
            navigationService.NavigateAsync("NavigationPage/LoginScreen");
        }
    }


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
        containerRegistry.RegisterForNavigation<MenuScreen, MenuScreenViewModel>();

    }
    
}

