using HaulMaster.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HaulMaster.Services
{
    public interface IBrokerService
    {
        List<Broker> GetBrokers();
        Broker GetBroker(int id);
        List<Broker> GetBrokersByName(string name);
        Broker AddBroker(Broker broker);
        Broker UpdateBroker(Broker broker);
        void DeleteBroker(int id);
    }
}
