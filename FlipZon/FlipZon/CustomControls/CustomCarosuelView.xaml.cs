using System.ComponentModel;

namespace FlipZon.CustomControls;

public partial class CustomCarosuelView : ContentView
{
    public CustomCarosuelView()
    {
        InitializeComponent();
        this.PropertyChanged += Handle_PropertyChanged;
        ItemSource = new ObservableCollection<ThumbNailModel>();
    }

    #region Bindable Properties
    public static readonly BindableProperty ItemSourceProperty =
            BindableProperty.Create("ItemSource", typeof(ObservableCollection<ThumbNailModel>), typeof(CustomCarosuelView), defaultBindingMode: BindingMode.TwoWay);

    public static readonly BindableProperty CarosuelHeightProperty = BindableProperty.Create(nameof(CarosuelHeight), typeof(double),
        typeof(CustomCarosuelView), 100.00, BindingMode.TwoWay);

    #endregion

    #region Properties
    public double CarosuelHeight
    {
        get => (double)GetValue(CarosuelHeightProperty);
        set => SetValue(CarosuelHeightProperty, value);
    }


    public ObservableCollection<ThumbNailModel> ItemSource
    {
        get { return (ObservableCollection<ThumbNailModel>)GetValue(ItemSourceProperty); }
        set
        {
            SetValue(ItemSourceProperty, value);
        }
    }
    #endregion

    #region Methods
    private void Handle_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "ItemSource")
        {
            SetItemSource();
        }
    }
    private void SetItemSource()
    {
        if (ItemSource != null)
            carouselView.HeightRequest = CarosuelHeight;
    }



    #endregion


    void SwipeGestureRecognizer_Swiped(System.Object sender, Microsoft.Maui.Controls.SwipedEventArgs e)
    {
        switch (e.Direction)
        {
            case SwipeDirection.Left:
                MoveCarousel(-1); 
                break;
            case SwipeDirection.Right:
                MoveCarousel(1);
                break;
        }
    }

    private void MoveCarousel(int direction)
    {

        try
        {
            int newPosition = carouselView.Position + direction;

            if (newPosition < 0)
            {
                newPosition = ItemSource.Count - 1;
            }
            else if (newPosition >= ItemSource.Count)
            {
                newPosition = 0;
            }

            carouselView.Position = newPosition;
        }
        catch (Exception ex)
        {

        }
    }

}

