namespace FlipZon.ViewModels
{
    public class ProductDetailsScreenViewModel : BaseViewModel
    {
        #region Properties
        private Product productDetails;
        public Product ProductDetails
        {
            get => productDetails;
            set { SetProperty(ref productDetails, value); }
        }

        private ObservableCollection<ThumbNailModel> images;
        public ObservableCollection<ThumbNailModel> Images
        {
            get => images;
            set { SetProperty(ref images, value); }
        }



        #endregion

        #region CTOR
        public ProductDetailsScreenViewModel(INavigationService navigationService,
                                            IDataService dataService,
                                            IRestService restService,
                                            IDataBase dataBase) : base(navigationService, dataService, restService,dataBase)
        {
        }
        #endregion

        #region Commands
        private DelegateCommand addItemToCartCommand;
        public DelegateCommand AddItemToCartCommand =>
            addItemToCartCommand ?? (addItemToCartCommand = new DelegateCommand (async () => { await ExecuteAddItemToCartCommand(); }));

        private DelegateCommand goToCartCommand;
        public DelegateCommand GoToCartCommand =>
            goToCartCommand ?? (goToCartCommand = new DelegateCommand(async () => { await ExecuteGoToCartCommand(); }));

        #endregion

        #region Methods

        private async Task ExecuteAddItemToCartCommand()
        {
            try
            {
                IsBusy = true;
                var addToCart = new CartRequestDto
                {
                    UserId = Preferences.Get(Constants.USER_ID, -1),
                    ProductId = ProductDetails.Id,
                    Quantity = 1,
                };
                var recordsInsertedCount= await DataBase.AddItemToCart(addToCart);
                if(recordsInsertedCount==0)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Product Added To cart", "Ok");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task ExecuteGoToCartCommand()
        {
            await NavigationService.NavigateAsync(Constants.CART_SCREEN);
        }

        #endregion

        #region Overriding Methods
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                IsBusy = true;
                if (parameters.ContainsKey(Constants.PRODUCT_ID))
                {
                    var productId = parameters.GetValue<int>(Constants.PRODUCT_ID);
                    var response = await RestService.GetProductDetails(productId);
                    if (response.Result != null)
                    {
                        ProductDetails = response.Result;
                        Images = new ObservableCollection<ThumbNailModel>();
                        foreach (var image in ProductDetails.Images)
                        {
                            var thumbnail = new ThumbNailModel
                            {
                                Name = image
                            };
                            Images.Add(thumbnail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}

