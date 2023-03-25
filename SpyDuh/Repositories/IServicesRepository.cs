using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface IServicesRepository
    {
        List<Service> GetAll(int id);

        Service GetById(int id);

        void Add(ServiceWithoutCost service);

        void Update(ServiceWithoutCost service);
        void Delete(int id);
    }
}