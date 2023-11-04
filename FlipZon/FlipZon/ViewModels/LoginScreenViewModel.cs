namespace FlipZon.ViewModels
{
    public class LoginScreenViewModel : BaseViewModel
    {
        #region Properties
      
        #endregion
        #region Commands
        private DelegateCommand signupCommand;
        public DelegateCommand SignupCommand =>
            signupCommand ?? (signupCommand =
                        new DelegateCommand(async () => { await ExecuteSignupCommand(); }));

        private DelegateCommand loginUserCommand;
        public DelegateCommand LoginUserCommand =>
            loginUserCommand ?? (loginUserCommand =
                        new DelegateCommand(async () => { await ExecuteLoginUserCommand(); }));

        private async Task ExecuteLoginUserCommand()
        {
            await NavigationService.NavigateAsync("HomeScreen");
        }




        #endregion
        public LoginScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase) : base(navigationService, dataService, restService, dataBase)
        {
        }

        #region Methods

        private async Task ExecuteSignupCommand()
        {
            await NavigationService.NavigateAsync("SignupScreen");
        }
        #endregion

    }
}

