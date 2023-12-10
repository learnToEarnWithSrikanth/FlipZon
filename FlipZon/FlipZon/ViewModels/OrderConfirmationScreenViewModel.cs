namespace FlipZon.ViewModels
{
    public class OrderConfirmationScreenViewModel : BaseViewModel
    {
        public OrderConfirmationScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase, IPopupNavigation popupNavigation) : base(navigationService, dataService, restService, dataBase, popupNavigation)
        {
        }
    }
}

