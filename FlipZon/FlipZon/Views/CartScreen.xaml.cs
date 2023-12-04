namespace FlipZon.Views;

public partial class CartScreen : ContentPage
{
    public CartScreen()
    {
        InitializeComponent();
      
    }



    async void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        try
        {
            var cartitem = sender as CartResponseDTO;

            // Check if the cast was successful
            if (cartitem == null)
            {
                // Handle the case where cartitem is null, e.g., log or display an error message.
                return;
            }

            var result = await DisplayPromptAsync("", "Enter Quantity", keyboard: Keyboard.Numeric, placeholder: "Enter Cart Quantity", cancel: "Cancel", accept: "Apply");

            if (result != null)
            {
                cartitem.Quantity = int.Parse(result);
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions appropriately, e.g., log the exception.
        }
    }

}

