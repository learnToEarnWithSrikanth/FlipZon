using Mopups.Interfaces;

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
        private double price;
        public double Price
        {
            get => price;
            set { SetProperty(ref price, value); }
        }

        private double deliveryFee;
        public double DeliveryFee
        {
            get => deliveryFee;
            set { SetProperty(ref deliveryFee, value); }
        }
        private double total;
        public double Total
        {
            get => total;
            set { SetProperty(ref total, value); }
        }

        private bool isCartEmpty;
        public bool IsCartEmpty
        {
            get => isCartEmpty;
            set { SetProperty(ref isCartEmpty, value); }
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

        public CartScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase, IPopupNavigation popupNavigation) : base(navigationService, dataService, restService, dataBase, popupNavigation)
        {
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
                IsBusy = true;
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
                    {
                        DisplayToast(string.Format("{0} deleted from the cart", cartItem.ProductInfo.Title), MessageType.Postive);
                        await GetCartItems();
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
       
        public void CaluclateCartPrice()
        {
            try
            {
                Price = 0;
                DeliveryFee = 0;
                Total = 0;
                foreach (var product in CartItems)
                {
                    product.DiscountedSubTotal = product.Quantity * product.ProductInfo.DiscountedPrice;
                    product.SubTotal = product.Quantity * product.ProductInfo.Price;
                    Price = Price + product.DiscountedSubTotal;
                }
                DeliveryFee = Price >= 1000 ? 0.00 : 10.00;
                Total = Price + DeliveryFee;
                DeliveryFee = Math.Round(DeliveryFee, 2);
                Price = Math.Round(Price, 2);
                Total = Math.Round(Total, 2);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task ExecuteUpdateCartQuantity(CartResponseDTO cartResponse)
        {
            try
            {
                IsBusy = true;
                var addToCart = new CartRequestDto
                {
                    Id=cartResponse.Id,
                    UserId = Preferences.Get(Constants.USER_ID, -1),
                    ProductId = cartResponse.ProductInfo.Id,
                    Quantity = cartResponse.Quantity,
                };
                var recordsInsertedCount = await DataBase.UpdateCartQuantity(addToCart);
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

        public async Task GetCartItems()
        {
            try
            {
                var response = await DataBase.GetAllCartItems(Preferences.Get(Constants.USER_ID, -1));
                if (response!=null && response.Count >0)
                {
                    CartItems = new ObservableCollection<CartResponseDTO>();
                    foreach (var item in response)
                    {
                        var productDetailsResponse= await RestService.GetProductDetails(item.ProductId);
                        if (productDetailsResponse.Result != null)
                        {
                            var cartItem = new CartResponseDTO
                            {
                                UserId=item.UserId,
                                Id = item.Id,
                                ProductInfo = productDetailsResponse.Result,
                                Quantity = item.Quantity,
                            };
                            CartItems.Add(cartItem);
                        }
                    }
                    IsCartEmpty = false;
                    CaluclateCartPrice();
                }
                else
                {
                    IsCartEmpty = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Overriding Methods
        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            try
            {
                IsBusy = true;
                await GetCartItems();
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

