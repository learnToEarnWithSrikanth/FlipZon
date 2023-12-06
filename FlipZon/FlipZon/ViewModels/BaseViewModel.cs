using Controls.UserDialogs.Maui;

namespace MauiSampleTest.ViewModels
{
    public class BaseViewModel : BindableBase, IDestructible, IPageLifecycleAware, INavigationAware, IInitialize, INavigationPageOptions
    {
        #region Public Variables
        public INavigationService NavigationService { get; }
        public IDataService DataService { get; }
        public IRestService RestService { get; }
        public IDataBase DataBase { get; }
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


        private DelegateCommand backCommand;
        public DelegateCommand BackCommand =>
            backCommand ?? (backCommand =
                        new DelegateCommand(ExecuteBackCommand));



        private DelegateCommand<Product> navigateToDetailsScreen;
        public DelegateCommand<Product> NavigateToDetailsScreen =>
            navigateToDetailsScreen ?? (navigateToDetailsScreen = new DelegateCommand<Product>(async (product) => { await ExecuteNavigateToDetailsScreen(product); }));
        #endregion

        #region CTOR
        public BaseViewModel(
            INavigationService navigationService,
            IDataService dataService,
            IRestService restService,
            IDataBase dataBase)
        {
            NavigationService = navigationService;
            DataService = dataService;
            RestService = restService;
            DataBase = dataBase;
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

        private async void ExecuteNaviagateToSearchScreenCommand()
        {
            await NavigationService.NavigateAsync(nameof(SearchScreen));
        }

        private async void ExecuteNaviagateToCartScreenCommand()
        {
            await NavigationService.NavigateAsync(nameof(CartScreen));
        }
        private async void ExecuteBackCommand()
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