namespace FlipZon.ViewModels
{
    public class CartScreenViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<CartResponseDTO> cartItems;
        public ObservableCollection<CartResponseDTO> CartItems
        {
            get => cartItems;
            set { SetProperty(ref cartItems, value); }
        }
        private ObservableCollection<CartResponseDTO> savedForLaterProducts;
        public ObservableCollection<CartResponseDTO> SavedForLaterProducts
        {
            get => savedForLaterProducts;
            set { SetProperty(ref savedForLaterProducts, value); }
        }

        #endregion

        #region Commands
        private DelegateCommand<CartResponseDTO> saveForLaterCommand;
        public DelegateCommand<CartResponseDTO> SaveForLaterCommand =>
            saveForLaterCommand ?? (saveForLaterCommand = new DelegateCommand<CartResponseDTO>(async (CartResponseDTO) => { await ExecuteSaveForLaterCommand(CartResponseDTO); }));

        private DelegateCommand<CartResponseDTO> deleteCommand;
        public DelegateCommand<CartResponseDTO> DeleteCommand =>
            deleteCommand ?? (deleteCommand = new DelegateCommand<CartResponseDTO>(async (CartResponseDTO) => { await ExecuteDeleteCommand(CartResponseDTO); }));

        #endregion

        #region CTOR
        public CartScreenViewModel(INavigationService navigationService,
                                   IDataService dataService, IRestService restService,IDataBase dataBase)
                                   : base(navigationService, dataService, restService,dataBase)
        {
            SavedForLaterProducts = new ObservableCollection<CartResponseDTO>();
        }
        #endregion

        #region Methods

        private async Task ExecuteSaveForLaterCommand(CartResponseDTO cartItem)
        {
            if (cartItem != null)
                savedForLaterProducts.Remove(cartItem);
            savedForLaterProducts?.Add(cartItem);

        }
        private async Task ExecuteDeleteCommand(CartResponseDTO cartItem)
        {
            try
            {
                if (cartItem != null)
                {
                    var cart = new CartRequestDto
                    {
                        Id = cartItem.Id,
                        ProductId = cartItem.ProductInfo.Id,
                        Quantity = cartItem.Quantity,
                        UserId = 1,
                    };
                    var response = await DataBase.DeleteCartItem(cart);
                    if (response != 0)
                        await GetCartItems();
                }
            }
            catch (Exception ex)
            {

            }

                
        }
        private async Task GetCartItems()
        {
            try
            {
                var response = await DataBase.GetAllCartItems();
                if (response != null)
                {
                    CartItems = new ObservableCollection<CartResponseDTO>();
                    foreach (var item in response)
                    {
                        var productDetailsResponse= await RestService.GetProductDetails(item.ProductId);
                        if (productDetailsResponse.Result != null)
                        {
                            var cartItem = new CartResponseDTO
                            {
                                Id = item.Id,
                                ProductInfo = productDetailsResponse.Result,
                                Quantity = item.Quantity,
                                SubTotal = item.Quantity * productDetailsResponse.Result.Price
                            };
                            CartItems.Add(cartItem);
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //  UserDialogs.Instance.HideLoading();
            }

        }
        #endregion

        #region Overriding Methods
        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            await GetCartItems();
        }
        #endregion
    }
}

