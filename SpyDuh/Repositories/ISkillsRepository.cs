using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface ISkillsRepository
    {
        List<Spy> GetSpecificSkills(string skill);
    }
}