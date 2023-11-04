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
        public AddAddressScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase) : base(navigationService, dataService, restService, dataBase)
        {

        }
        #endregion

        #region Methods
        private async Task ExecuteSaveAddressCommand()
        {
            if (
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
                Address=Address,
                Pincode=Pincode,
                PhoneNumber = PhoneNumber,
                DoorNo=DoorNo,
                Name=Name,
                Id=Id
            };
            var parameters = new NavigationParameters();
            if (isEditAddreesMode == true)
            {
                parameters.Add("EditedAddress", addNewAddress);
            }
            else
            {
                parameters.Add("NewAddress", addNewAddress);
            }
            await NavigationService.NavigateAsync(nameof(AddressListScreen), parameters);
        }
        #endregion

        #region OverRiding Methods
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            if (parameters.ContainsKey("EditableAddress"))
            {
                var ediatbaleAddress= parameters.GetValue<AddressModel>("EditableAddress");
                IsEditAddreesMode = true;
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

