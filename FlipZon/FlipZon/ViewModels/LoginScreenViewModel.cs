namespace FlipZon.ViewModels
{
    public class LoginScreenViewModel : BaseViewModel
    {
        #region Properties

        private string email;
        public string Email
        {
            get => email;
            set { SetProperty(ref email, value); }
        }

        private string password;
        public string Password
        {
            get => password;
            set { SetProperty(ref password, value); }
        }
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
            try
            {
                var isValidAccount = await ValiateAccountInformation();
                var account = new SignupModel
                {
                    Email = Email,
                    Password = Password,
                };
                if (isValidAccount)
                {
                    var response= await DataBase.GetAccount(account);
                    if (response == null)
                    {
                        DisplayToast("InValid Credentails! Please Try again", MessageType.Negative);
                        return;
                    }
                    Preferences.Set(Constants.USER_NAME, response.Name);
                    Preferences.Set(Constants.EMAIL, response.Email);
                    Preferences.Set(Constants.USER_ID, response.Id);
                    DisplayToast("Login Success", MessageType.Postive);
                    await NavigationService.NavigateAsync(nameof(HomeScreen));
                }
            }
            catch (Exception ex)
            {

            }
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

        private async Task<bool> ValiateAccountInformation()
        {
            bool isValid = true;
            try
            {
                if (string.IsNullOrEmpty(Email) ||
                   string.IsNullOrEmpty(Password)) 
                    
                {
                    isValid = false;
                    await Application.Current.MainPage.DisplayAlert("Enter all the Fields", "Please fill all the Required Field", "Ok");
                    return isValid = false;
                }
            }
            catch
            {

            }
            return isValid;
        }
        #endregion

    }
}

