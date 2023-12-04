﻿using Microsoft.Maui.Controls;
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

        private DelegateCommand<CartResponseDTO> editCartQuantityCommand;
        public DelegateCommand<CartResponseDTO> EditCartQuantityCommand =>
            editCartQuantityCommand ?? (editCartQuantityCommand = new DelegateCommand<CartResponseDTO>(async (CartResponseDTO) => { await ExecuteEditCartQuantityCommand(CartResponseDTO); }));

        private async Task ExecuteEditCartQuantityCommand(CartResponseDTO cartResponseDTO)
        {
          
        }

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
        private void GetMockData()
        {
            try
            {
               // Assuming this code is in a method or constructor

                // Create some mock products
                Product product1 = new Product
                {
                    Id = 1,
                    Title = "Product 1",
                    Description = "Description 1",
                    Price = 100,
                    DiscountPercentage = 10,
                    Rating = 4.5,
                    Stock = 50,
                    Brand = "Brand 1",
                    Category = "Category 1",
                    Thumbnail = "thumbnail1.jpg",
                    Images = new List<string> { "image1.jpg", "image2.jpg" }
                };

                Product product2 = new Product
                {
                    Id = 2,
                    Title = "Product 2",
                    Description = "Description 2",
                    Price = 150,
                    DiscountPercentage = 15,
                    Rating = 4.0,
                    Stock = 30,
                    Brand = "Brand 2",
                    Category = "Category 2",
                    Thumbnail = "thumbnail2.jpg",
                    Images = new List<string> { "image3.jpg", "image4.jpg" }
                };

                // Create some mock CartResponseDTO items
                CartResponseDTO cartItem1 = new CartResponseDTO
                {
                    Id = 1,
                    ProductInfo = product1,
                    Quantity = 2,
                    SubTotal = (int)(product1.DiscountedPrice * 2),
                    IsEditQuantityEnabled = true
                };

                CartResponseDTO cartItem2 = new CartResponseDTO
                {
                    Id = 2,
                    ProductInfo = product2,
                    Quantity = 3,
                    SubTotal = (int)(product2.DiscountedPrice * 3),
                    IsEditQuantityEnabled = false
                };

                // Create the ObservableCollection and add mock items
                ObservableCollection<CartResponseDTO> cartItems = new ObservableCollection<CartResponseDTO>
                {
                    cartItem1,
                    cartItem2
                };

                // Set the CartItems property
                CartItems = cartItems;
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
            // await GetCartItems();
            GetMockData();
        }
        #endregion
    }
}

