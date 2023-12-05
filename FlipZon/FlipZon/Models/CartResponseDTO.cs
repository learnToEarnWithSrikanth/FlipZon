using System;
namespace FlipZon.Models
{
    public class CartResponseDTO:BindableBase
    {
        public int Id { get; set; }
        public Product ProductInfo { get; set; }

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set
            {
                SetProperty(ref quantity, value);
            }
        }
        private double discountedSubTotal;
        public double  DiscountedSubTotal
        {
            get => discountedSubTotal;
            set
            {
                SetProperty(ref discountedSubTotal, value);
            }
        }
        private double subTotal;
        public double SubTotal
        {
            get => subTotal;
            set
            {
                SetProperty(ref subTotal, value);
            }
        }
    }
}

