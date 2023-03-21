using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface IFriendRepository
    {
        List<Spy> GetFriends(int id);
    }
}