using HaulMaster.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HaulMaster.Services
{
    public interface IBrokerService
    {
        List<Broker> GetBrokers();
        Broker GetBroker(int id);
        Broker AddBroker(Broker broker);
    }
}
