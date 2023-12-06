namespace FlipZon.ViewModels
{
    public class SignupScreenViewModel : BaseViewModel
    {
        #region Properties
        private string name;
        public string Name
        {
            get => name;
            set { SetProperty(ref name, value); }
        }

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

        private string confirmpassword;
        public string Confirmpassword
        {
            get => confirmpassword;
            set { SetProperty(ref confirmpassword, value); }
        }

        #endregion
        #region Commands
        private DelegateCommand loginCommad;
        public DelegateCommand LoginCommad =>
            loginCommad ?? (loginCommad =
                        new DelegateCommand(async () => { await ExecuteLoginCommad(); }));

        private DelegateCommand signupCommand;
        public DelegateCommand SignupCommand =>
            signupCommand ?? (signupCommand =
                        new DelegateCommand(async () => { await ExecuteSignupCommand(); }));

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

        private async Task ExecuteSignupCommand()
        {
           
            try
            {
                var isValidAccount = await ValiateAccountInformation();
                var account = new SignupModel
                {
                    Email = Email,
                    Name = Name,
                    Password = Password,
                };
                if (isValidAccount)
                {
                    var response = await DataBase.CreateAccount(account);
                    if (response == 1)
                    {
                        DisplayToast("Account Created Successfully, Please login to use the app",MessageType.Postive);
                        await NavigationService.NavigateAsync(nameof(LoginScreen));
                    }
                    else
                    {
                        DisplayToast("Account Created Failed, Please try again Later",MessageType.Negative);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async Task<bool> ValiateAccountInformation()
        {
            bool isValid = true;
            try
            {
                if (string.IsNullOrEmpty(Name) ||
                      string.IsNullOrEmpty(Email) ||
                      string.IsNullOrEmpty(Password) ||
                      string.IsNullOrEmpty(Confirmpassword))
                {
                    isValid = false;
                    await Application.Current.MainPage.DisplayAlert("Enter all the Fields", "Please fill all the Required Field", "Ok");
                    return isValid=false;
                }

                if (Password != Confirmpassword)
                {
                    isValid = false;
                    DisplayToast("Password and confirm password not matched",MessageType.Negative);
                    return isValid=false;
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

