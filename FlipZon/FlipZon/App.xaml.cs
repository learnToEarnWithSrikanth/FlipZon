namespace MauiSampleTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           // MainPage = DetermineMainPage();
        }

        //private Page DetermineMainPage()
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
    }
}
