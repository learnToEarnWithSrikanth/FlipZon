using Android.App;
using Android.Content.PM;
using Controls.UserDialogs.Maui;

namespace MauiSampleTest
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [IntentFilter(new[] { Platform.Intent.ActionAppAction },
                  Categories = new[] { global::Android.Content.Intent.CategoryDefault })]
    public class MainActivity : MauiAppCompatActivity
    {
        private readonly INavigationService _navigationService;

        public MainActivity()
        {
           
        }

        protected override void OnResume()
        {
            base.OnResume();

            Platform.OnResume(this);
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);

            Platform.OnNewIntent(intent);
        }

        public override async void OnBackPressed()
        {
            var currentScreen =  App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            if (currentScreen is HomeScreen)
                await ShowCloseAppConfirmationScreen();
            else
                await App.Current.MainPage.Navigation.PopAsync();

        }

        private async Task ShowCloseAppConfirmationScreen()
        {
            var response = await UserDialogs.Instance.ConfirmAsync("Would like to close the app", "Close the app", "Yes", "No");
            if (response)
                Finish();

        }
    }
}
