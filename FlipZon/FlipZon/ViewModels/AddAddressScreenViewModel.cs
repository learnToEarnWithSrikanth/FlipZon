namespace FlipZon.ViewModels
{
    public class AddAddressScreenViewModel : BaseViewModel
    {
        #region Properties
        private AddressModel addressDetails;
        public AddressModel AddressDetails
        {
            get => addressDetails;
            set { SetProperty(ref addressDetails, value); }
        }
        private string name;
        public string Name
        {
            get => name;
            set { SetProperty(ref name, value); }
        }
        private string title = MobileLabel.Add_New_Address;
        public string Title
        {
            get => title;
            set { SetProperty(ref title, value); }
        }
        private string buttonText = MobileLabel.Save_Address;
        public string ButtonText
        {
            get => buttonText;
            set { SetProperty(ref buttonText, value); }
        }
        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set { SetProperty(ref phoneNumber, value); }
        }
        private string address;
        public string Address
        {
            get => address;
            set { SetProperty(ref address, value); }
        }
        private string email;
        public string Email
        {
            get => email;
            set { SetProperty(ref email, value); }
        }

        private string doorNo;
        public string DoorNo
        {
            get => doorNo;
            set { SetProperty(ref doorNo, value); }
        }
        private string pincode;
        public string Pincode
        {
            get => pincode;
            set { SetProperty(ref pincode, value); }
        }
        private bool isEditAddreesMode;
        public bool IsEditAddreesMode
        {
            get => isEditAddreesMode;
            set { SetProperty(ref isEditAddreesMode, value); }
        }
        private int id;
        public int Id
        {
            get => id;
            set { SetProperty(ref id, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand saveAddressCommand;
        public DelegateCommand SaveAddressCommand =>
            saveAddressCommand ?? (saveAddressCommand = new DelegateCommand(async () => { await ExecuteSaveAddressCommand(); }));

        #endregion

        #region CTOR
        public AddAddressScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase, IPopupNavigation popupNavigation) : base(navigationService, dataService, restService, dataBase, popupNavigation)
        {
        }
        #endregion

        #region Methods
        private async Task ExecuteSaveAddressCommand()
        {
            try
            {
                if (string.IsNullOrEmpty(Email) ||
                    string.IsNullOrEmpty(Address) ||
                    string.IsNullOrEmpty(Name) ||
                    string.IsNullOrEmpty(PhoneNumber) ||
                    string.IsNullOrEmpty(DoorNo) ||
                    string.IsNullOrEmpty(Pincode))
                {
                    await Application.Current.MainPage.DisplayAlert("Enter all the Fields", "Please fill all the Required Field", "Ok");
                    return;
                }
                var addNewAddress = new AddressModel
                {
                    Address = Address,
                    Pincode = Pincode,
                    PhoneNumber = PhoneNumber,
                    DoorNo = DoorNo,
                    Name = Name,
                    IsSelected = true,
                    UserId = Preferences.Get(Constants.USER_ID, -1),
                    Email = Email,
                    Id = Id
                };
                IsBusy = true;
                var result = await DataBase.GetAllAddressByUserId(Preferences.Get(Constants.USER_ID, -1));
                if (result != null && result.Count > 0)
                {
                    await DataBase.UpdateAllAddress();
                }
                if (isEditAddreesMode == true)
                {
                    var response = await DataBase.UpdateAddress(addNewAddress);
                    if (response == 1)
                        DisplayToast("Address Updated successfully", MessageType.Postive);
                }
                else
                {
                    var response = await DataBase.AddAddress(addNewAddress);
                    if (response == 1)
                        DisplayToast("Address added successfully", MessageType.Postive);
                }
                await NavigationService.NavigateAsync(nameof(AddressListScreen));
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

        #region OverRiding Methods
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (parameters.ContainsKey(Constants.EDITABLE_ADDRESS))
            {
                var ediatbaleAddress= parameters.GetValue<AddressModel>(Constants.EDITABLE_ADDRESS);
                IsEditAddreesMode = true;
                Title = MobileLabel.Edit_Address;
                ButtonText = MobileLabel.Update_Address;
                Email = ediatbaleAddress?.Email;
                Name = ediatbaleAddress?.Name;
                Pincode = ediatbaleAddress?.Pincode;
                DoorNo = ediatbaleAddress.DoorNo;
                Address = ediatbaleAddress.Address;
                PhoneNumber = ediatbaleAddress.PhoneNumber;
                id = ediatbaleAddress.Id;
            }
        }
        #endregion

    }
}

