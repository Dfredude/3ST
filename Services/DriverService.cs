using FinalProject.Data;
using HaulMaster.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HaulMaster.Services
{
    public class DriverService : IDriverService
    {
        private readonly FinalProjectContext _context;
        public DriverService(FinalProjectContext context) {
            _context = context;
        }
        public void AddDriver(Driver driver)
        {
            _context.Driver.Add(driver);
            _context.SaveChanges();

        }

        public Driver GetDriver(int id)
        {
            throw new NotImplementedException();
        }

        public List<Driver> GetDrivers()
        {
            return _context.Driver.ToList();
        }

        public List<Driver> drivers = new List<Driver>()
        {
            new Driver() { ID = 1, FirstName = "John", LastName = "Doe", Phone = "28913138931" , License="dsada"}
        };
    }
}
