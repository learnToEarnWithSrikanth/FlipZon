using System;
namespace FlipZon.Models
{
    public class AddressModel:BindableBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Pincode { get; set; }
        public string Address { get; set; }
        public string DoorNo { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set { SetProperty(ref isSelected, value); }
        }
    }
}

