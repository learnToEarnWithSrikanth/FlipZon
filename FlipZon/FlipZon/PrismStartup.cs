using FlipZon.ViewModels;
using FlipZon.Views;

namespace MauiSampleTest;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
                .OnAppStart("NavigationPage/HomeScreen");
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
    }
}

