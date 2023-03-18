using SpyDuh.Models;
using SpyDuh.Utils;

namespace SpyDuh.Repositories
{
    public class SkillsRepository : BaseRepository, ISkillsRepository
    {
        public SkillsRepository(IConfiguration configuration) : base(configuration) { }

        public List<Spy> GetSpecificSkills(string skill)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT s.id as sId, s.name, s.userName, s.email, s.isMember, s.DateCreated,
                            sk.skillName, sk.id as skId, sj.skillLevel
                        FROM Spy s
                        LEFT JOIN SkillJoin sj on s.id = sj.spyId
                        LEFT JOIN Skill sk on sj.skillId = sk.id
                        WHERE sk.skillName LIKE @skill";
                    cmd.Parameters.AddWithValue("@skill", "%" + skill + "%");

                    var reader = cmd.ExecuteReader();

                    var spySkills = new List<Spy>();

                    while (reader.Read())
                    {
                        var skillId = DbUtils.GetInt(reader, "sId");
                        var existingSkill = spySkills.FirstOrDefault(x => x.Id == skillId);
                        if (existingSkill == null)
                        {
                            existingSkill = new Spy()
                            {
                                Id = skillId,
                                Name = DbUtils.GetString(reader, "name"),
                                UserName = DbUtils.GetString(reader, "userName"),
                                Email = DbUtils.GetString(reader, "email"),
                                IsMemeber = DbUtils.GetBool(reader, "isMember"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                Skills = new List<Skill>()
                            };
                        }
                        spySkills.Add(existingSkill);

                        existingSkill.Skills.Add(new Skill()
                        {
                            Id = DbUtils.GetInt(reader, "skId"),
                            SkillName = DbUtils.GetString(reader, "skillName"),
                            SkillLevel = DbUtils.GetInt(reader, "skillLevel")
                        });
                    }
                    reader.Close();

                    return spySkills;
                }
            }
        }
    }
}
