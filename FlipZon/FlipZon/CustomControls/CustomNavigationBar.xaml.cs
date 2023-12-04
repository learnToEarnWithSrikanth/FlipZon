namespace FlipZon.CustomControls;

public partial class CustomNavigationBar : ContentView
{
    public CustomNavigationBar()
    {
        InitializeComponent();
    }

    #region Properties

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    #endregion

    #region Bindable Properties
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
        typeof(CustomNavigationBar), "", BindingMode.TwoWay);

    #endregion

    #region Commands
    public static readonly BindableProperty BackCommandProperty = BindableProperty.Create(nameof(BackCommand), typeof(DelegateCommand), typeof(CustomNavigationBar), null);
    public DelegateCommand BackCommand
    {
        get => (DelegateCommand)GetValue(BackCommandProperty);
        set => SetValue(BackCommandProperty, value);
    }
    #endregion
}

