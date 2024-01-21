using FinalProject.Data;
using HaulMaster.Models;

namespace HaulMaster.Services
{
    public class ClientService : IClientService
    {
        private readonly FinalProjectContext _context;
        public ClientService(FinalProjectContext context)
        {
            _context = context;
        }

        public void AddClient(Client client)
        {
            throw new NotImplementedException();
        }

        public Client GetClient(string USDOT)
        {
            throw new NotImplementedException();
        }

        public List<Client> GetClients()
        {
            return _context.Client!.ToList();
        }
    }
}
