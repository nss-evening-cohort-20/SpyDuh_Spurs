using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface IFriendRepository
    {
        Spy GetFriends(int id);
    }
}