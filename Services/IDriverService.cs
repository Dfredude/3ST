using HaulMaster.Models;

namespace HaulMaster.Services
{
    public interface IDriverService
    {
        List<Driver> GetDrivers();
        Driver GetDriver(int id);
        void AddDriver(Driver driver);
    }
}
