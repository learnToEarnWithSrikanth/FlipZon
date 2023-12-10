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
        private bool isAddressListEmpty;
        public bool IsAddressListEmpty
        {
            get => isAddressListEmpty;
            set { SetProperty(ref isAddressListEmpty, value); }
        }
        #endregion

        #region CTOR

        public AddressListScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase, IPopupNavigation popupNavigation) : base(navigationService, dataService, restService, dataBase, popupNavigation)
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

        private DelegateCommand<AddressModel> deleteAddressCommand;
        public DelegateCommand<AddressModel> DeleteAddressCommand =>
            deleteAddressCommand ?? (deleteAddressCommand = new DelegateCommand<AddressModel>(async (AddressModel) => await ExecuteDeleteAddressCommand(AddressModel)));

        private DelegateCommand placeOrderCommand;
        public DelegateCommand PlaceOrderCommand =>
            placeOrderCommand ?? (placeOrderCommand = new DelegateCommand(async () => await ExecutePlaceOrderCommand()));

        #endregion

        #region Methods

        private async Task ExecutePlaceOrderCommand()
        {
            await DataBase.DeleteAllCartItemsForUser(Preferences.Get(Constants.USER_ID, -1));
            await NavigationService.NavigateAsync(nameof(OrderConfirmationScreen));
        }

        private async Task ExecuteEditAddressCommand(AddressModel editableAddressModel)
        {
            var parameters = new NavigationParameters
            {
                { Constants.EDITABLE_ADDRESS, editableAddressModel }
            };
            await NavigationService.NavigateAsync(nameof(AddAddressScreen), parameters);
        }

        private async void ExecuteAddNewAddressCommand()
        {
            await NavigationService.NavigateAsync(nameof(AddAddressScreen));
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
        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            try
            {
                IsBusy = true;
                await GetAddressList();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
        public async Task GetAddressList()
        {
            try
            {
                var response = await DataBase.GetAllAddressByUserId(Preferences.Get(Constants.USER_ID, -1));
                AddressList = new ObservableCollection<AddressModel>(response);
                IsAddressListEmpty = AddressList?.Count > 0 ? false : true;
            }
            catch (Exception ex)
            {

            }
        }
        public async Task ExecuteDeleteAddressCommand(AddressModel addressModel)
        {
            try
            {
                var response = await DataBase.DeleteAddress(addressModel);
                if (response==1)
                {
                    UserDialogs.Instance.ShowToast(new ToastConfig()
                    {
                        Icon = "bin.png",
                        Message = "Address Deleted Successfully",
                    });
                    await GetAddressList();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }

}

