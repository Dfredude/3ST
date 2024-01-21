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

        public List<Broker> GetBrokersByName(string name)
        {
            return _context.Broker!.Where(b => b.Name.Contains(name)).ToList();
        }

        public Broker UpdateBroker(Broker broker)
        {
            Broker UpdatedBroker = _context.Broker!.Update(broker).Entity;
            if (_context.SaveChanges() == 0)
            {
                throw new DbUpdateException("Unable to update " + typeof(Broker).Name + " " + broker.Name + " in DB");
            }
            return UpdatedBroker;
        }

        public List<Broker> GetBrokers() => _context.Broker!.ToList();

        public void DeleteBroker(int id)
        {
            Broker broker = _context.Broker!.Find(id);
            if (broker == null)
            {
                throw new DbUpdateException("Unable to find " + typeof(Broker).Name + " with ID " + id);
            }
            _context.Broker!.Remove(broker);
            if (_context.SaveChanges() == 0)
            {
                throw new DbUpdateException("Unable to delete " + typeof(Broker).Name + " with ID " + id);
            }
        }
    }
}
