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

        public int  SubTotal { get; set; }

        private bool isEditQuantityEnabled;
        public bool IsEditQuantityEnabled
        {
            get => isEditQuantityEnabled;
            set { SetProperty(ref isEditQuantityEnabled, value); }
        }


    }
}

