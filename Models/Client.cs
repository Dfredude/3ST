using System.ComponentModel.DataAnnotations;

namespace HaulMaster.Models
{
    public class Client
    {
        [Key]
        public string USDOT { get; set; } = default!;
        public string MC { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Email { get; set; } = default!;
        public float? quickPayPercentage { get; set; }
        public float? quickPayTime { get; set; }
        public string? Notes { get; set; }
        public bool Blacklisted { get; set; } = false;
    }
}
