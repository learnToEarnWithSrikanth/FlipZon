namespace FlipZon.ViewModels
{
    public class SignupScreenViewModel : BaseViewModel
    {
        #region Commands
        private DelegateCommand loginCommad;
        public DelegateCommand LoginCommad =>
            loginCommad ?? (loginCommad =
                        new DelegateCommand(async () => { await ExecuteLoginCommad(); }));

        #endregion

        #region CTOR
        public SignupScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase) : base(navigationService, dataService, restService, dataBase)
        {
        }
        #endregion 

        #region Methods

        private async Task ExecuteLoginCommad()
        {
            await NavigationService.NavigateAsync("LoginScreen");
        }
        #endregion
    }
}

