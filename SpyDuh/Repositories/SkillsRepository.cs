using SpyDuh.Models;
using SpyDuh.Utils;

namespace SpyDuh.Repositories
{
    public class SkillsRepository : BaseRepository, ISkillsRepository
    {
        public SkillsRepository(IConfiguration configuration) : base(configuration) { }

        public List<Skill> GetSpecificSkills(string skill)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT sp.id spId, sp.name, sp.userName, sp.email, sp.isMember, sp.dateCreated, 
                               s.id sId, s.skillName, s.skillLevel, s.spyId 
                        FROM Spy sp 
                        JOIN Skill s on s.spyId = sp.id WHERE s.skillName LIKE @skill";
                    cmd.Parameters.AddWithValue("@skill", "%" + skill + "%");

                    var reader = cmd.ExecuteReader();

                    var skills = new List<Skill>();

                    while (reader.Read())
                    {
                        var skillId = DbUtils.GetInt(reader, "sId");
                        var existingSkill = skills.FirstOrDefault(x => x.Id == skillId);
                        if (existingSkill == null)
                        {

                            existingSkill = new Skill()
                            {
                                Id = DbUtils.GetInt(reader, "sId"),
                                SkillName = DbUtils.GetString(reader, "skillName"),
                                SkillLevel = DbUtils.GetInt(reader, "skillLevel"),
                                SpyId = DbUtils.GetInt(reader, "spyId"),
                                spy = new Spy() //using the spy class from the models
                                {
                                    Id = DbUtils.GetInt(reader, "spId"),
                                    Name = DbUtils.GetString(reader, "name"),
                                    UserName = DbUtils.GetString(reader, "userName"),
                                    Email = DbUtils.GetString(reader, "email"),
                                    IsMember = DbUtils.GetBool(reader, "isMember"),
                                    DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                                }                                
                            };
                        }
                        skills.Add(existingSkill);
                    }
                    reader.Close();

                    return skills;
                }
            }
        }
    }
}
