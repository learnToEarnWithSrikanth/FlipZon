using FlipZon.ViewModels;
using Mopups.Pages;
using Mopups.Services;

namespace FlipZon.Views;


public partial class MenuScreen : PopupPage
{
    private MenuScreenViewModel vm;
    public MenuScreen()
    {
        InitializeComponent();
        vm = BindingContext as MenuScreenViewModel;
    }

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        MopupService.Instance.PopAsync(true);
    }
}

