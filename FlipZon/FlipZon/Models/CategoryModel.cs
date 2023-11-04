namespace FlipZon.Models
{
    public class Category:BindableBase
    {
        public string Name { get; set; }

        private bool isActive;
        public bool IsActive
        {
            get => isActive;
            set { SetProperty(ref isActive, value); }
        }
    }

}

