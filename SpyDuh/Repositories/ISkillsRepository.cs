using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface ISkillsRepository
    {
        List<Skill> GetSpecificSkills(string skill);
    }
}