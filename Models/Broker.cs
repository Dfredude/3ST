namespace HaulMaster.Models
{
    public class Broker
    {
        public int ID { get; set; }
        public string Name { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Notes { get; set; } = default!;
        public string USDOT { get; set; } = default!;
    }
}
