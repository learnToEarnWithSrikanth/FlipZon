using Mopups.Interfaces;

namespace FlipZon.ViewModels
{
    public class MenuScreenViewModel : BaseViewModel
    {
        public MenuScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase, IPopupNavigation popupNavigation) : base(navigationService, dataService, restService, dataBase, popupNavigation)
        {
        }
    }
}

