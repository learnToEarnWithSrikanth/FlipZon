namespace MauiSampleTest.ViewModels
{
    public class BaseViewModel : BindableBase, IDestructible, IPageLifecycleAware, INavigationAware, IInitialize, INavigationPageOptions
    {
        #region Public Variables
        public INavigationService NavigationService { get; }
        public IDataService DataService { get; }
        public IRestService RestService { get; }
        public IDataBase DataBase { get; }
        public IPopupNavigation  PopupNavigation { get; }
        public bool ClearNavigationStackOnNavigation => DataService.ClearDetailPageStack;
        public int skipCount=0;
        public int productsExistsInCollection=0;
        public int totalNoOfProducts=0;
        public int limitCount=10;
        #endregion

        #region Properties
        private ObservableCollection<Product> products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products
        {
            get => products;
            set { SetProperty(ref products, value); }
        }
        private bool canShowPassword;
        public bool CanShowPassword
        {
            get => canShowPassword;
            set { SetProperty(ref canShowPassword, value); }
        }
        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set { SetProperty(ref isBusy, value); }
        }
        private bool isInternetConnectionAvailable=false;
        public bool IsInternetConnectionAvailable
        {
            get => isInternetConnectionAvailable;
            set { SetProperty(ref isInternetConnectionAvailable, value); }
        }
        #endregion

        #region Commands
        private DelegateCommand navigateToProductsScreen;
        public DelegateCommand NavigateToProductsScreen =>
            navigateToProductsScreen ?? (navigateToProductsScreen =
                        new DelegateCommand(async () => { await ExecuteNavigatToProductsScreen(); }));

        private DelegateCommand togglePasswordVisiblityCommand;
        public DelegateCommand TogglePasswordVisiblityCommand =>
            togglePasswordVisiblityCommand ?? (togglePasswordVisiblityCommand =
                        new DelegateCommand(ExecuteTogglePasswordVisiblityCommand));

        private DelegateCommand naviagateToCartScreenCommand;
        public DelegateCommand NaviagateToCartScreenCommand =>
            naviagateToCartScreenCommand ?? (naviagateToCartScreenCommand =
                        new DelegateCommand(ExecuteNaviagateToCartScreenCommand));

        private DelegateCommand naviagateToSearchScreenCommand;
        public DelegateCommand NaviagateToSearchScreenCommand =>
            naviagateToSearchScreenCommand ?? (naviagateToSearchScreenCommand =
                        new DelegateCommand(ExecuteNaviagateToSearchScreenCommand));

        private DelegateCommand naviagateToHomeScreenCommand;
        public DelegateCommand NaviagateToHomeScreenCommand =>
            naviagateToHomeScreenCommand ?? (naviagateToHomeScreenCommand =
                        new DelegateCommand(async() => await ExecuteNaviagateToHomeScreenCommand()));

        private DelegateCommand logoutCommand;
        public DelegateCommand LogoutCommand =>
            logoutCommand ?? (logoutCommand =
                        new DelegateCommand(async () => await ExecuteLogoutCommand()));

  

        private DelegateCommand naviagateToAddressScreenCommand;
        public DelegateCommand NaviagateToAddressScreenCommand =>
            naviagateToAddressScreenCommand ?? (naviagateToAddressScreenCommand =
                        new DelegateCommand(async () => await ExcecuteNavigateToAddressListScreen()));


        private DelegateCommand backCommand;
        public DelegateCommand BackCommand =>
            backCommand ?? (backCommand =
                        new DelegateCommand(ExecuteBackCommand));

        private DelegateCommand naviagateToMenuScreenCommand;
        public DelegateCommand NaviagateToMenuScreenCommand =>
            naviagateToMenuScreenCommand ?? (naviagateToMenuScreenCommand = new DelegateCommand(async () => { await ExecuteNaviagateToMenuScreenCommand(); }));


        private DelegateCommand<Product> navigateToDetailsScreen;
        public DelegateCommand<Product> NavigateToDetailsScreen =>
            navigateToDetailsScreen ?? (navigateToDetailsScreen = new DelegateCommand<Product>(async (product) => { await ExecuteNavigateToDetailsScreen(product); }));
        #endregion

        #region CTOR
        public BaseViewModel(
            INavigationService navigationService,
            IDataService dataService,
            IRestService restService,
            IDataBase dataBase,
            IPopupNavigation popupNavigation)
        {
            NavigationService = navigationService;
            DataService = dataService;
            RestService = restService;
            DataBase = dataBase;
            PopupNavigation = popupNavigation;
        }
        #endregion

        #region Overriding Methods 
        public virtual void OnResume()
        {

        }

        public virtual void OnSleep()
        {

        }

        public virtual void OnAppearing()
        {

        }

        public virtual void OnDisappearing()
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public void Destroy()
        {
        }

        public async Task PopModalAsync()
        {
            var navResult = await NavigationService.GoBackAsync();
        }
        #endregion

        #region Methods

        public bool CheckNetworkConnection()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
                return true;
            return false;
        }

        public void DisplayToast(string message,MessageType messageType)
        {
            var icon = messageType == MessageType.Postive ? "tick.png" : "wrong.png";
            UserDialogs.Instance.ShowToast(new ToastConfig()
            {
                Icon = icon,
                Message = message,
            });
        }

        private async Task ExecuteLogoutCommand()
        {
            var response= await  UserDialogs.Instance.ConfirmAsync("Would you like Logout?", "Logout", "Yes", "No");
            if(response)
            {
                Preferences.Clear(Constants.USER_ID);
                Preferences.Clear(Constants.USER_NAME);
                Preferences.Clear(Constants.EMAIL);
                await NavigationService.NavigateAsync(nameof(LoginScreen));
            }
        }

        public async Task ExcecuteNavigateToAddressListScreen()
        {
            var currentPage = GetCurrentScreen();
            if (currentPage is AddressListScreen)
                return;
            await NavigationService.NavigateAsync(nameof(AddressListScreen));
        }

        private async Task ExecuteNaviagateToMenuScreenCommand()
        {
            await NavigationService.NavigateAsync(nameof(MenuScreen));
            // await PopupNavigation.PushAsync(new MenuScreen(), true);
        }

        public Page GetCurrentScreen()
        {
           return  App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
        }

        private async Task ExecuteNaviagateToHomeScreenCommand()
        {
            var currentPage = GetCurrentScreen();
            if (currentPage is HomeScreen)
                return;
            await NavigationService.NavigateAsync(nameof(HomeScreen));
        }

        public async Task ExecuteNavigatToProductsScreen()
        {
            var currentPage = GetCurrentScreen();
            if (currentPage is ProductsScreen)
                return;
            await NavigationService.NavigateAsync(nameof(ProductsScreen));
        }

        public async void ExecuteNaviagateToSearchScreenCommand()
        {
            var currentPage = GetCurrentScreen();
            if (currentPage is SearchScreen)
                return;
            await NavigationService.NavigateAsync(nameof(SearchScreen));
        }

        public async void ExecuteNaviagateToCartScreenCommand()
        {
            var currentPage = GetCurrentScreen();
            if (currentPage is CartScreen)
                return;
            await NavigationService.NavigateAsync(nameof(CartScreen));
        }
        public async void ExecuteBackCommand()
        {
            await NavigationService.GoBackAsync();
        }

        private void ExecuteTogglePasswordVisiblityCommand()
        {
            CanShowPassword = !CanShowPassword;
        }
        public async Task GetProducts(int skipCount, int limitCount)
        {
            try
            {
                var productsList = await RestService.GetProducts(skipCount, limitCount);
                if (productsList != null && productsList?.Result?.Products != null)
                {
                    foreach (var product in productsList?.Result?.Products)
                    {
                        Products?.Add(product);
                    }
                    totalNoOfProducts = (int)(productsList?.Result?.Total);
                    productsExistsInCollection = Products.Count();
                    skipCount = (int)(productsList?.Result?.Skip);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //UserDialogs.Instance.HideLoading();
            }

        }

      
        public async Task ExecuteNavigateToDetailsScreen(Product product)
        {
            if (product != null)
            {
                var parameters = new NavigationParameters
                {
                    { Constants.PRODUCT_ID, product.Id }
                };
                await NavigationService.NavigateAsync(nameof(ProductDetailsScreen), parameters);
            }
        }
        #endregion
    }
}