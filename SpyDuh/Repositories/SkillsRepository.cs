using SpyDuh.Models;
using SpyDuh.Utils;

namespace SpyDuh.Repositories
{
    public class SkillsRepository : BaseRepository, ISkillsRepository
    {
        public SkillsRepository(IConfiguration configuration) : base(configuration) { }

        public Skill GetSpecificSkills(string skill)
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
                        JOIN Skill s on s.spyId = sp.id WHERE s.skillName = @skill";
                    cmd.Parameters.AddWithValue("@skill", skill);

                    var reader = cmd.ExecuteReader();

                    Skill skills = null;

                    if (reader.Read())
                    {
                        skills = new Skill()
                        {
                            Id = DbUtils.GetInt(reader, "sId"),
                            SkillName = DbUtils.GetString(reader, "skillName"),
                            SkillLevel = DbUtils.GetInt(reader, "skillLevel"),
                            SpyId = DbUtils.GetInt(reader, "spyId"),
                            spy = new Spy()
                            {
                                Id = DbUtils.GetInt(reader, "spId"),
                                Name = DbUtils.GetString(reader, "name"),
                                UserName = DbUtils.GetString(reader, "userName"),
                                Email = DbUtils.GetString(reader, "email"),
                                IsMemeber = DbUtils.GetBool(reader, "isMember"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),

                            }
                        };
                    }
                    reader.Close();

                    return skills;
                }
            }
        }
    }
}
