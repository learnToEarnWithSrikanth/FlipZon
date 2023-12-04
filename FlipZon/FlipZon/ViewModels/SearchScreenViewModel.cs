namespace FlipZon.ViewModels
{
    public class SearchScreenViewModel : BaseViewModel
    {

        #region Properties
        private ObservableCollection<Product> productsSearchList;
        public ObservableCollection<Product> ProductsSearchList
        {
            get => productsSearchList;
            set { SetProperty(ref productsSearchList, value); }
        }
        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                SetProperty(ref searchText, value);
                ExecuteSearchProductsCommand();
            }
        }
        #endregion

        #region Commands

        #endregion
        public SearchScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase) : base(navigationService, dataService, restService, dataBase)
        {
        }
        private async void ExecuteSearchProductsCommand()
        {
            try
            {

                if (string.IsNullOrEmpty(SearchText))
                {
                    ProductsSearchList?.Clear();
                    return;
                }
             
                var response = await RestService.SearchProducts(searchText);                if (response?.Result != null)
                {
                    ProductsSearchList = new ObservableCollection<Product>(response?.Result.Products);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    
}

