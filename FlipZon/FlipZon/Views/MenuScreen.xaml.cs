using Mopups.Pages;
using Mopups.Services;

namespace FlipZon.Views;


public partial class MenuScreen : PopupPage
{
    public MenuScreen()
    {
        InitializeComponent();
    }

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        MopupService.Instance.PopAsync(true);
    }
}

