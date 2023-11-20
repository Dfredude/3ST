using System.ComponentModel;

namespace FinalProject.Models
{
    public class Product
    {
        public int ID { get; set; }
        [DisplayName("Name of good")]
        public string ProductName { get; set; }
        [DisplayName("Picture")]
        public string ImageName { get; set; }
        public string Description { get; set; } = default!;
        public float Price { get; set; } = default!;
        public bool LTL { get; set; } = false;
    }
}
