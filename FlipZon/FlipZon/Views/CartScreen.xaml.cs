using System.Threading;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using FlipZon.ViewModels;
using Microsoft.Maui;
using static System.Net.Mime.MediaTypeNames;

namespace FlipZon.Views;

public partial class CartScreen : ContentPage
{
    CartScreenViewModel vm;

    public CartScreen()
    {
        InitializeComponent();

        // This event is triggered after the XAML is initialized and the BindingContext is set
        this.BindingContextChanged += OnBindingContextChanged;
    }

    private void OnBindingContextChanged(object sender, EventArgs e)
    {
        // Now you can safely access the BindingContext
        vm = BindingContext as CartScreenViewModel;

        // Unsubscribe from the event to avoid multiple calls
        this.BindingContextChanged -= OnBindingContextChanged;
    }



    async void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        try
        {
            int maxquantity = 10;
            if (sender is View view && view.BindingContext is CartResponseDTO cartItem)
            {
                if (cartItem == null)
                {
                    return;
                }

                var result = await DisplayPromptAsync("", "Enter Quantity", keyboard: Keyboard.Numeric, placeholder: "Enter Cart Quantity", cancel: "Cancel", accept: "Apply");

                if (result != null)
                {
                    if(int.Parse(result) > maxquantity)
                    {
                        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                        string text = $"Sorry Maximum of {maxquantity} is allowed per order";
                        ToastDuration duration = ToastDuration.Short;
                        var toast = Toast.Make(text, duration);
                        await toast.Show(cancellationTokenSource.Token);
                    }
                    cartItem.Quantity = int.Parse(result) > maxquantity ? maxquantity : int.Parse(result);
                    await vm?.ExecuteUpdateCartQuantity(cartItem);
                    await vm?.GetCartItems();
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions appropriately, e.g., log the exception.
        }
    }

}

