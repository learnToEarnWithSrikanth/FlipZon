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
            set { SetProperty(ref quantity, value); }
        }

        public double  DiscountedSubTotal
        {
            get
            {
                return ProductInfo.DiscountedPrice * quantity;
            }
        }
        public double SubTotal
        {
            get
            {
                return ProductInfo.Price * quantity;
            }
        }
    }
}

