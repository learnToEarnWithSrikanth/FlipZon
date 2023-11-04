namespace FlipZon.CustomControls;

public partial class ProductTileView : ContentView
{
    public ProductTileView()
    {
        InitializeComponent();
    }

    #region Properties
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
    public string ThumbNail
    {
        get => (string)GetValue(ThumbNailProperty);
        set => SetValue(ThumbNailProperty, value);
    }
    public double OrginalPrice
    {
        get => (double)GetValue(OrginalPriceProperty);
        set => SetValue(OrginalPriceProperty, value);
    }
    public double DiscountedPrice
    {
        get => (double)GetValue(DiscountedPriceProperty);
        set => SetValue(DiscountedPriceProperty, value);
    }
    public double Rating
    {
        get => (double)GetValue(RatingProperty);
        set => SetValue(RatingProperty, value);
    }
    #endregion

    #region Bindable Properties

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string),
        typeof(ProductTileView), string.Empty, BindingMode.TwoWay);


    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string),
        typeof(ProductTileView), string.Empty, BindingMode.TwoWay);

    public static readonly BindableProperty OrginalPriceProperty = BindableProperty.Create(nameof(OrginalPrice), typeof(double),
        typeof(ProductTileView), 0.00, BindingMode.TwoWay);

    public static readonly BindableProperty DiscountedPriceProperty = BindableProperty.Create(nameof(DiscountedPrice), typeof(double),
        typeof(ProductTileView), 0.00, BindingMode.TwoWay);

    public static readonly BindableProperty ThumbNailProperty = BindableProperty.Create(nameof(ThumbNail), typeof(string),
    typeof(ProductTileView), string.Empty, BindingMode.TwoWay);

    public static readonly BindableProperty RatingProperty = BindableProperty.Create(nameof(Rating), typeof(double),
        typeof(ProductTileView), 0.00, BindingMode.TwoWay);
    #endregion

    #region Commands
    public static readonly BindableProperty ProductTappedCommandProperty = BindableProperty.Create(nameof(ProductTappedCommand), typeof(DelegateCommand<Product>), typeof(ProductTileView), null);
    public DelegateCommand<Product> ProductTappedCommand
    {
        get => (DelegateCommand<Product>)GetValue(ProductTappedCommandProperty);
        set => SetValue(ProductTappedCommandProperty, value);
    }
    #endregion
}

