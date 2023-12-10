namespace FlipZon.ViewModels
{
    public class HomeScreenViewModel : BaseViewModel
    {
       
        #region Properties
        private ObservableCollection<ThumbNailModel> images;
        public ObservableCollection<ThumbNailModel> Images
        {
            get => images;
            set { SetProperty(ref images, value); }
        }
        #endregion

        #region Commands
        private DelegateCommand tryAgainCommand;
        public DelegateCommand TryAgainCommand =>
            tryAgainCommand ?? (tryAgainCommand = new DelegateCommand(async () => { await ExecuteTryAgainCommand(); }));

        #endregion

        #region CTOR
        public HomeScreenViewModel(INavigationService navigationService, IDataService dataService, IRestService restService, IDataBase dataBase, IPopupNavigation popupNavigation) : base(navigationService, dataService, restService, dataBase, popupNavigation)
        {
        }

        #endregion

        #region Methods
        private async Task ExecuteTryAgainCommand()
        {
            await LoadData();
        }



        #endregion

        #region Overriding Methods

        public override async void Initialize(INavigationParameters parameters)
        {
            try
            {
                base.Initialize(parameters);
                await LoadData();
            }
            catch (Exception ex)
            {

            }
        }

        private async Task LoadData()
        {
            try
            {
                IsBusy = true;
                if (!CheckNetworkConnection())
                {
                    IsInternetConnectionAvailable = false;
                    return;
                }
                IsInternetConnectionAvailable = true;
                await GetProducts(skipCount, limitCount);
                GetThumbnails();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
        

        private void GetThumbnails()
        {
            try
            {
                Images = new ObservableCollection<ThumbNailModel>()
                {
                    new ThumbNailModel
                    {
                        Name = "https://source.unsplash.com/1024x768/?clothes"
                    },
                    new ThumbNailModel
                    {
                        Name = "https://source.unsplash.com/1024x768/?mobiles"
                    },
                    new ThumbNailModel
                    {
                        Name = "https://source.unsplash.com/1024x768/?shopping"
                    },
                    new ThumbNailModel
                    {
                        Name = "https://source.unsplash.com/1024x768/?laptops"
                    }
                };
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}

