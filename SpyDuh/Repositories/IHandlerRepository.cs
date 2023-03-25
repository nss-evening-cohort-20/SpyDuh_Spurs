using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface IHandlerRepository
    {
        Agency listSpies(int id);
    }
}