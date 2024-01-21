using HaulMaster.Models;

namespace HaulMaster.Services
{
    public interface IDispatcherService
    {
        List<Dispatcher> GetDispatchers();
        Dispatcher GetDispatcher(int id);
        Dispatcher AddDispatcher(Dispatcher dispatcher);
    }
}
