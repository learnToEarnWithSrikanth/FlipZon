namespace FlipZon.ViewModels
{
    public class ProductsScreenViewModel : BaseViewModel
    {
    
        public ProductsScreenViewModel(INavigationService navigationService,
                                        IDataService dataService,
                                        IRestService restService,
                                        IDataBase dataBase) :
                                        base(navigationService, dataService, restService, dataBase)
        {
        }

        #region Commands

        private DelegateCommand<Category> filterProductsByCategoryCommand;
        public DelegateCommand<Category> FilterProductsByCategoryCommand =>
            filterProductsByCategoryCommand ?? (filterProductsByCategoryCommand =
                        new DelegateCommand<Category>(async (selectedCategory) => { await ExecuteFilterProductsByCategoryCommand(selectedCategory); }));

        private DelegateCommand<string> toggleProductsView;
        public DelegateCommand<string> ToggleProductsView =>
            toggleProductsView ?? (toggleProductsView =
                        new DelegateCommand<string>( (selectedView) => { ExecuteToggleProductsView(selectedView); }));

        private DelegateCommand loadMoreProducts;
        public DelegateCommand LoadMoreProducts =>
            loadMoreProducts ?? (loadMoreProducts =
                        new DelegateCommand(async () => {await ExecuteLoadMoreProducts(); }));


        #endregion

        #region Properties
        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories
        {
            get => categories;
            set { SetProperty(ref categories, value); }
        }

        private bool canShowListView;
        public bool CanShowListView
        {
            get => canShowListView;
            set { SetProperty(ref canShowListView, value); }
        }
        private bool isLoadingMoreProducts;
        public bool IsLoadingMoreProducts
        {
            get => isLoadingMoreProducts;
            set { SetProperty(ref isLoadingMoreProducts, value); }
        }
        private string buttonText="load More";
        public string ButtonText
        {
            get => buttonText;
            set { SetProperty(ref buttonText, value); }
        }
        private bool isMoreButtonEnabled=true;
        public bool IsMoreButtonEnabled
        {
            get => isMoreButtonEnabled;
            set { SetProperty(ref isMoreButtonEnabled, value); }
        }

        private bool isAllTabSelected = true;
        public bool IsAllTabSelected
        {
            get => isAllTabSelected;
            set { SetProperty(ref isAllTabSelected, value); }
        }

        #endregion

        #region Methods
        private void ExecuteToggleProductsView(string selectedView)
        {
            if (selectedView == "ListView")
                CanShowListView = true;
            else
                CanShowListView = false;
        }
        private async Task ExecuteLoadMoreProducts()
        {
            try
            {
                IsLoadingMoreProducts = true;
                skipCount = skipCount + 10;
                await GetProducts(skipCount, limitCount);
                if (productsExistsInCollection >= totalNoOfProducts)
                {
                    skipCount = 0;
                    ButtonText = "End of Products";
                    IsMoreButtonEnabled = false;
                    return;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoadingMoreProducts = false;
            }
        }
        private async Task GetCategories()
        {
            try
            {
                var response = await RestService.GetCategories();
                if (response != null && response?.Result != null)
                {
                     Categories = new ObservableCollection<Category>
                     {
                          new Category { IsActive = true, Name = "ALL" }
                     };
                    foreach (var category in response.Result)
                    {
                        var productCategory = new Category
                        {
                            IsActive = false,
                            Name = category
                        };
                        Categories.Add(productCategory);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async Task ExecuteFilterProductsByCategoryCommand(Category selectedCategory)
        {
            try
            {
                IsBusy = true;
                foreach (var category in Categories)
                {
                    category.IsActive = false;
                }
                var filteredCategory = Categories.Where(x => x.Name == selectedCategory.Name).FirstOrDefault();
                filteredCategory.IsActive = true;
                IsAllTabSelected = false;

                if (selectedCategory.Name == "ALL")
                {
                    IsAllTabSelected = true;
                    Products = new ObservableCollection<Product>();
                    await GetProducts(skipCount, limitCount);
                    return;
                }
                await FetchProductsBasedOnCategory(selectedCategory);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }

        }

        private async Task FetchProductsBasedOnCategory(Category selectedCategory)
        {

            try
            {
                var response = await RestService.GetProductsByCategory(selectedCategory.Name);
                if (response?.Result != null)
                {
                    Products = new ObservableCollection<Product>(response?.Result.Products);
                }
            }
            catch (Exception ex)
            {

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
                await GetProducts(skipCount, limitCount);
                await GetCategories();
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

