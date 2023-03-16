using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface ISkillsRepository
    {
        Skill GetSpecificSkills(string skill);
    }
}