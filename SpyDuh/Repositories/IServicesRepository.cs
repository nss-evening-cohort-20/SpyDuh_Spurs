using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface IServicesRepository
    {
        List<Service> GetAll();

        Service GetById(int id);

        void Add(Service service);

        void Update(Service service);
        void Delete(int id);
    }
}