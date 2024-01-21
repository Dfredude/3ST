using HaulMaster.Models;

namespace HaulMaster.Services
{
    public interface IClientService
    {
        List<Client> GetClients();
        Client GetClient(string USDOT);
        void AddClient(Client client);
    }
}
