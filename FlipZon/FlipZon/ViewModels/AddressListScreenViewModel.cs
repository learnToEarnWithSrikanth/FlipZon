namespace FlipZon.ViewModels
{
    public class AddressListScreenViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<AddressModel> addressList;
        public ObservableCollection<AddressModel> AddressList
        {
            get => addressList;
            set { SetProperty(ref addressList, value); }
        }   
        #endregion

        #region CTOR
        public AddressListScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase) : base(navigationService, dataService, restService, dataBase)
        {

        }
        #endregion

        #region Commands
        private DelegateCommand<AddressModel> addressSelectionChangedCommand;
        public DelegateCommand<AddressModel> AddressSelectionChangedCommand =>
            addressSelectionChangedCommand ?? (addressSelectionChangedCommand = new DelegateCommand<AddressModel>(ExecuteAddressSelectionChangedCommand));

        private DelegateCommand addNewAddressCommand;
        public DelegateCommand AddNewAddressCommand =>
            addNewAddressCommand ?? (addNewAddressCommand = new DelegateCommand(ExecuteAddNewAddressCommand));

        private DelegateCommand<AddressModel> editAddressCommand;
        public DelegateCommand<AddressModel> EditAddressCommand =>
            editAddressCommand ?? (editAddressCommand = new DelegateCommand<AddressModel>( async (AddressModel) => await ExecuteEditAddressCommand(AddressModel)));

        private async Task ExecuteEditAddressCommand(AddressModel editableAddressModel)
        {
            var parameters = new NavigationParameters
            {
                { "EditableAddress", editableAddressModel }
            };
            await NavigationService.NavigateAsync(nameof(AddAddressScreen),parameters);
        }


        #endregion

        #region Methods

        private async void ExecuteAddNewAddressCommand()
        {
            await NavigationService.NavigateAsync("AddAddressScreen");
        }

        public void GetAddressList()
        {
            AddressList = new ObservableCollection<AddressModel>
            {
                new AddressModel
                {
                    Id=1,
                    Name="Srikanth",
                    PhoneNumber="1234567890",
                    Address="Sbi Street Podalakur",
                    Pincode="524345",
                    DoorNo="1-23",
                    IsSelected=true,
                },
                new AddressModel
                {
                    Id=2,
                    Name="Srikanth",
                    PhoneNumber="1234567890",
                    Address="Sbi Street Podalakur",
                    Pincode="524345",
                    DoorNo="1-23",
                }
            };
        }

        private void ExecuteAddressSelectionChangedCommand(AddressModel addressModel)
        {
            foreach (var address in AddressList)
            {
                address.IsSelected = false;
            }
            var filteredAddress = AddressList?.Where(x => x.Id == addressModel.Id).FirstOrDefault();
            filteredAddress.IsSelected = true;
        }
        #endregion

        #region Overriding Methods
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            GetAddressList();
            if (parameters.ContainsKey("NewAddress"))
            {
                var addedAddress= parameters.GetValue<AddressModel>("NewAddress");
                AddressList.Add(addedAddress);
                ExecuteAddressSelectionChangedCommand(addedAddress);
            }
            if (parameters.ContainsKey("EditedAddress"))
            {
                var addedAddress = parameters.GetValue<AddressModel>("EditedAddress");
                var selectedAddress = AddressList.Where(x => x.Id == addedAddress.Id).FirstOrDefault();
                AddressList[AddressList.IndexOf(selectedAddress)] = addedAddress;
                ExecuteAddressSelectionChangedCommand(addedAddress);
            }
        }
        #endregion
    }

}

