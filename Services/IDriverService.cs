using HaulMaster.Models;

namespace HaulMaster.Services
{
    public interface IDriverService
    {
        List<Driver> GetDrivers();
        Driver GetDriver(int id);
        Driver AddDriver(Driver driver);
    }
}
