using FinalProject.Models;

namespace HaulMaster.Models
{
    public class Order
    {
        public int ID { get; set; }
        public bool IsGuest { get; set; } = true;
        public List<Product> Products { get; set; } = new List<Product>();
    }
}