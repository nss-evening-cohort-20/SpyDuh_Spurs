using SpyDuh.Models;
using SpyDuh.Utils;

namespace SpyDuh.Repositories
{
    public class SpyRepository : BaseRepository, ISpyRepository
    {
        public SpyRepository(IConfiguration configuration) : base(configuration) { }

        public Spy getEnemies(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT 
S.Id AS SID, S.Name, S.UserName, S.Email, S.IsMember, S.DateCreated,

SK.Id AS SpySkillId, SK.skillName AS SpySkill,

SJ.skillLevel AS SpySkillLevel,

E.Id, E.SpyId, E.EnemySpyId,

SRVJ.Cost AS SpyCost,

SRV.Id AS SpyServiceId, SRV.ServiceName as SpyServiceName,

ES.Id AS EnemyId, ES.Name AS EnemyName, ES.UserName AS EnemyUserName, ES.Email AS EnemyEmail, ES.IsMember AS EnemyIsMember, ES.DateCreated AS EnemyDateCreated,

ESJ.skillLevel AS EnemySkillLevel,

ESK.Id AS EnemySkillId, ESK.SkillName AS EnemySkill,

ESRVJ.Cost AS EnemyCost,

ESRV.Id AS EnemyServiceId, ESRV.ServiceName as EnemyServiceName

FROM Spy S

LEFT JOIN SkillJoin SJ
ON SJ.SpyId = S.id

LEFT JOIN Skill SK
ON SK.id = SJ.SkillId

LEFT JOIN [ServiceJoin] SRVJ
ON SRVJ.spyId = S.id

LEFT JOIN [Service] SRV
ON SRV.id = SRVJ.serviceId

LEFT JOIN Enemy E 
ON E.spyId = S.id

LEFT JOIN Spy ES
ON E.EnemySpyId = ES.id

LEFT JOIN SKillJoin ESJ
ON ESJ.SpyId = ES.id

LEFT JOIN
Skill ESK
ON ESK.id = ESJ.SkillId

LEFT JOIN [ServiceJoin] ESRVJ
ON ESRVJ.spyId = ES.id

LEFT JOIN [Service] ESRV
ON ESRV.id = ESRVJ.serviceId

where e.spyId = 2 or e.enemySpyId = 2
order by e.enemySpyId asc";

                    DbUtils.AddParameter(cmd, "@id", id);
                    var reader = cmd.ExecuteReader();
                    var spy = new Spy();
                    var enemies = new List<Spy>();
                    var spyIds = new List<int>();
                    while (reader.Read())
                    {
                        var spyId = DbUtils.GetInt(reader, "SpyId");
                        var enemyId = DbUtils.GetInt(reader, "EnemyId");
                        var existingEnemy = enemies.FirstOrDefault(x => x.Id == enemyId || x.Id == spyId);

                        if(existingEnemy == null)
                        {

                            if (spyId == id)
                            {
                                existingEnemy = new Spy()
                                {
                                    Id = enemyId,
                                    Name = DbUtils.GetString(reader, "EnemyName"),
                                    UserName = DbUtils.GetString(reader, "EnemyUserName"),
                                    Email = DbUtils.GetString(reader, "EnemyEmail"),
                                    IsMemeber = DbUtils.GetBoolean(reader, "EnemyIsMember"),
                                    DateCreated = DbUtils.GetDateTime(reader, "EnemyDateCreated"),
                                    Skills = new List<Skill>(),
                                    Services = new List<Service>()
                                };

                                enemies.Add(existingEnemy);
                            }

                            if (enemyId == id)
                            {
                                existingEnemy = new Spy()
                                {
                                    Id = spyId,
                                    Name = DbUtils.GetString(reader, "Name"),
                                    UserName = DbUtils.GetString(reader, "UserName"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    IsMemeber = DbUtils.GetBoolean(reader, "IsMember"),
                                    DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                    Skills = new List<Skill>(),
                                    Services = new List<Service>()
                                };

                                enemies.Add(existingEnemy);
                            }


                        }

                        if (spyId == id)
                        {

                            var skillId = DbUtils.GetInt(reader, "EnemySkillId");
                            var exisitingSkill = existingEnemy.Skills.FirstOrDefault(x => x.Id == skillId);
                            if (exisitingSkill == null)
                            {
                                exisitingSkill = new Skill()
                                {
                                    Id = skillId,
                                    SkillName = DbUtils.GetString(reader, "EnemySkill"),
                                    SkillLevel = DbUtils.GetInt(reader, "EnemySkillLevel")

                                };
                                existingEnemy.Skills.Add(exisitingSkill);
                            }

                            var serviceId = DbUtils.GetInt(reader, "EnemyServiceId");
                            var existngService = existingEnemy.Services.FirstOrDefault(x => x.Id == serviceId);
                            if (existngService == null)
                            {
                                existngService = new Service()
                                {
                                    Id = serviceId,
                                    ServiceName = DbUtils.GetString(reader, "EnemyServiceName"),
                                    Cost = DbUtils.GetInt(reader, "EnemyCost")
                                };
                                existingEnemy.Services.Add(existngService);
                            }
                        }

                        if (enemyId == id)
                        {

                            var skillId = DbUtils.GetInt(reader, "SpySkillId");
                            var exisitingSkill = existingEnemy.Skills.FirstOrDefault(x => x.Id == skillId);
                            if (exisitingSkill == null)
                            {
                                exisitingSkill = new Skill()
                                {
                                    Id = skillId,
                                    SkillName = DbUtils.GetString(reader, "SpySkill"),
                                    SkillLevel = DbUtils.GetInt(reader, "SpySkillLevel")

                                };
                                existingEnemy.Skills.Add(exisitingSkill);
                            }

                            var serviceId = DbUtils.GetInt(reader, "SpyServiceId");
                            var existngService = existingEnemy.Services.FirstOrDefault(x => x.Id == serviceId);
                            if (existngService == null)
                            {
                                existngService = new Service()
                                {
                                    Id = serviceId,
                                    ServiceName = DbUtils.GetString(reader, "SpyServiceName"),
                                    Cost = DbUtils.GetInt(reader, "SpyCost")
                                };
                                existingEnemy.Services.Add(existngService);
                            }
                        }

                        spy = new Spy
                        {
                            Id = id,
                            Name = DbUtils.GetString(reader, "Name"),
                            UserName = DbUtils.GetString(reader, "UserName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            IsMemeber = DbUtils.GetBoolean(reader, "IsMember"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            Enemies= enemies

                        };
                    }

                    reader.Close();
                    return spy;

                }

            }
        }
    }
}
