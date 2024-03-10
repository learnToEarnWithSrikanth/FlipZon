using Prism.Navigation;

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
        try
        {
            if (Preferences.ContainsKey(Constants.USER_ID))
            {
                var id = Preferences.Get(Constants.USER_ID, -1);
                if (id != -1)
                {
                    //if (IsLoginWithFigerPrintConfigured())
                    //{
                    //    navigationService.NavigateAsync("NavigationPage/FingerPrintScreen");
                    //}
                    //else
                    //{
                    //    navigationService.NavigateAsync("NavigationPage/HomeScreen");
                    //}
                    navigationService.NavigateAsync("NavigationPage/HomeScreen");
                }
            }
            else
            {
                // User is not logged navigate to LoginScreen
                navigationService.NavigateAsync("NavigationPage/LoginScreen");
            }
        }
        catch(Exception ex)
        {

        }

    }

    private static bool IsLoginWithFigerPrintConfigured()
    {
        bool isFigerPrintAllowed = false;
        try
        {
            if (Preferences.ContainsKey(Constants.LOGIN_WITH_FINGER_PRINT))
            {
                isFigerPrintAllowed = Preferences.Get(Constants.LOGIN_WITH_FINGER_PRINT, false);
            }
        }
        catch
        {

        }
        return isFigerPrintAllowed;
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
        containerRegistry.RegisterForNavigation<OrderConfirmationScreen, OrderConfirmationScreenViewModel>();
        containerRegistry.RegisterForNavigation<FingerPrintScreen, FingerPrintScreenViewModel>();
    }
    
}

