using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface ISpyRepository
    {
        void Add(Spy spy);
        List<Spy> GetAll();
    }
}