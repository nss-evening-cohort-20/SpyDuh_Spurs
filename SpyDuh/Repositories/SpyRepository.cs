using System;
using Microsoft.AspNetCore.Mvc;
using SpyDuh.Models;
using SpyDuh.Utils;




namespace SpyDuh.Repositories
{
    public class SpyRepository : BaseRepository, ISpyRepository
    {
        public SpyRepository(IConfiguration configuration) : base(configuration) { }


        public List<EnemySpy> listEnemies(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT 

ES.Id AS EnemyId, ES.Name AS EnemyName, ES.UserName AS EnemyUserName, ES.Email AS EnemyEmail, ES.IsMember AS EnemyIsMember, ES.DateCreated AS EnemyDateCreated,

ESJ.skillLevel AS EnemySkillLevel,

ESK.Id AS EnemySkillId, ESK.SkillName AS EnemySkill,

ESRVJ.Cost AS EnemyCost,

ESRV.Id AS EnemyServiceId, ESRV.ServiceName as EnemyServiceName

FROM Spy S

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

where e.spyId = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    var reader = cmd.ExecuteReader();
                    var spy = new EnemySpy();
                    var enemies = new List<EnemySpy>();
                    while (reader.Read())
                    {
                        var enemyId = DbUtils.GetInt(reader, "EnemyId");
                        var existingEnemy = enemies.FirstOrDefault(x => x.Id == enemyId);

                        if (existingEnemy == null)
                        {

                            
                                existingEnemy = new EnemySpy()
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

                    reader.Close();
                    return enemies;

                }

            }
        }






        // finding enimies that the user spy has made and spies that have made that same user spy an emeny without the need for the addEnemy post method
        // returns spy as header
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

where e.spyId = @id or e.enemySpyId = @id
order by e.enemySpyId asc";

                    DbUtils.AddParameter(cmd, "@id", id);
                    var reader = cmd.ExecuteReader();
                    var enemySpy = new EnemySpy();
                    var spy = new Spy();
                    var enemies = new List<EnemySpy>();
                    while (reader.Read())
                    {
                        var spyId = DbUtils.GetInt(reader, "SpyId");
                        var enemyId = DbUtils.GetInt(reader, "EnemyId");
                        var existingEnemy = enemies.FirstOrDefault(x => x.Id == enemyId || x.Id == spyId);

                        if(existingEnemy == null)
                        {

                            if (spyId == id)
                            {
                                existingEnemy = new EnemySpy()
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
                                existingEnemy = new EnemySpy()
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

        public void Add(NewSpy spy)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {              

                    cmd.CommandText = @$"
INSERT INTO Spy(name, userName, email, isMember, handlerId, DateCreated)
OUTPUT INSERTED.id
VALUES(@name, @userName, @email, {1}, @handlerId, @DateCreated)";              

                    DbUtils.AddParameter(cmd, "@name", spy.Name);
                    DbUtils.AddParameter(cmd, "@userName", spy.UserName);
                    DbUtils.AddParameter(cmd, "@email", spy.Email);
                    //DbUtils.AddParameter(cmd, "@isMember", 1);
                    DbUtils.AddParameter(cmd, "@handlerId", spy.HandlerId );
                    DbUtils.AddParameter(cmd, "@DateCreated", DateTime.Now);

                    spy.Id = (int)cmd.ExecuteScalar();
                }

            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using( var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
DELETE FROM Spy
WHERE id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddEnemy(int spyId, int enemySpyId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using( var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Enemy(spyId, enemySpyId)
OUTPUT INSERTED.id
VALUES(@spyId, @enemySpyId),(@spyId2, @enemySpyId2)";

                    DbUtils.AddParameter(cmd, "@spyId", spyId);
                    DbUtils.AddParameter(cmd, "@EnemySpyId", enemySpyId);
                    DbUtils.AddParameter(cmd, "@spyId2", enemySpyId);
                    DbUtils.AddParameter(cmd, "@enemySpyId2", spyId);

                    cmd.ExecuteNonQuery();

                }
            }
        }

    }
}
