using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface IAssignmentRepository
    {
        Assignment GetAssignment(int id);
    }
}