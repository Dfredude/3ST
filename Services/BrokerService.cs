using FinalProject.Data;
using HaulMaster.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HaulMaster.Services
{
    public class BrokerService : IBrokerService
    {
        private readonly FinalProjectContext _context;
        public BrokerService(FinalProjectContext context) {
            _context = context;
        }
        public Broker AddBroker(Broker broker)
        {
            Broker AddedBroker = _context.Broker!.Add(broker).Entity;
            if (_context.SaveChanges() == 0)
            {
                throw new DbUpdateException("Unable to add " + typeof(Broker).Name + " to DB");
            }
            return AddedBroker;
        }

        public Broker GetBroker(int id)
        {
            return _context.Broker!.Find(id);
        }

        public List<Broker> GetBrokers() => _context.Broker!.ToList();
    }
}
