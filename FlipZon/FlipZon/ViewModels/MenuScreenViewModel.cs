namespace FlipZon.ViewModels
{
    public class MenuScreenViewModel : BaseViewModel
    {
        public MenuScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase, IPopupNavigation popupNavigation) : base(navigationService, dataService, restService, dataBase, popupNavigation)
        {
        }

        #region Properties
        private string userName;
        public string UserName
        {
            get => userName;
            set { SetProperty(ref userName, value); }
        }
        #endregion 
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            UserName = Preferences.Get(Constants.USER_NAME,"Un Known User");
        }
    }
    
}

