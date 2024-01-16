using System.ComponentModel;

namespace HaulMaster.Models
{
    public class Driver
    {
        public int ID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; } = default!;
        [DisplayName("Last Name")]
        public string LastName { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string License { get; set; } = default!; 
    }
}
