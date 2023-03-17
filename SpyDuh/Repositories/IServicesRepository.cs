using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface IServicesRepository
    {
        List<Service> GetAll();

        Service GetById(int id);

        Service Add(Service service);
    }
}