using System;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace FlipZon.ViewModels
{
    public class FingerPrintScreenViewModel : BaseViewModel
    {
        #region Commands
        private DelegateCommand addFingerPrintCommand;
        public DelegateCommand AddFingerPrintCommand =>
            addFingerPrintCommand ?? (addFingerPrintCommand = new DelegateCommand(async () => await ExecuteAddFingerPrintCommand()));

        #endregion

        #region Properties
        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set { SetProperty(ref errorMessage, value); }
        }
        private FingerPrintStatus fingerPrintStatus;
        public FingerPrintStatus FingerPrintStatus
        {
            get => fingerPrintStatus;
            set { SetProperty(ref fingerPrintStatus, value); }
        }
        #endregion

        #region CTOR
        public FingerPrintScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase, IPopupNavigation popupNavigation) : base(navigationService, dataService, restService, dataBase, popupNavigation)
        {
            FingerPrintStatus = FingerPrintStatus.Failure;
            
        }
        #endregion CTOR

        #region Methods
        private async Task ExecuteAddFingerPrintCommand()
        {
            try
            {

                var isFingerPrintAvailable = await CrossFingerprint.Current.IsAvailableAsync();

                if (!isFingerPrintAvailable)
                {
                    ErrorMessage = "No biometrics available";
                    return;
                }

                var authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Unlock FlipZon", "Enter phone screen lock pattern,PIN,password or fingerprint"));

                if (!string.IsNullOrWhiteSpace(authResult.ErrorMessage))
                {
                    ErrorMessage = authResult.ErrorMessage;
                    FingerPrintStatus = FingerPrintStatus.Failure;
                    return;
                }

                if (authResult.Authenticated)
                {
                    FingerPrintStatus = FingerPrintStatus.Success;
                    await NavigationService.NavigateAsync(nameof(HomeScreen));
                }
            }
            catch(Exception ex)
            {

            }
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            Task.Run(ExecuteAddFingerPrintCommand);
        }
        #endregion
    }


}

