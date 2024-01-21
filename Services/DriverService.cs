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
        public Driver AddDriver(Driver driver)
        {
            Driver addedDriver = _context.Driver.Add(driver).Entity;
            if (_context.SaveChanges() == 0)
            {
                throw new DbUpdateException("Unable to add " + typeof(Driver).Name + " to DB");
            }
            return addedDriver;
        }

        public Driver GetDriver(int id)
        {
            throw new NotImplementedException();
        }

        public List<Driver> GetDrivers()
        {
            return _context.Driver.ToList();
        }
    }
}
