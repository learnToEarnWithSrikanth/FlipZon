namespace FlipZon.CustomControls;

public partial class CustomLoadingIndicator : ContentView
{
    public CustomLoadingIndicator()
    {
        InitializeComponent();
    }
    #region Properties
    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    public string LoadingText
    {
        get => (string)GetValue(LoadingTextProperty);
        set => SetValue(LoadingTextProperty, value);
    }


    #endregion

    #region Bindable Properties
    public static readonly BindableProperty LoadingTextProperty = BindableProperty.Create(nameof(LoadingText), typeof(string),
           typeof(CustomLoadingIndicator), "Loading", BindingMode.TwoWay);

    public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool),
        typeof(CustomLoadingIndicator), false, BindingMode.TwoWay);

    #endregion
}

