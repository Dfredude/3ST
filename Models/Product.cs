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
    }
}
