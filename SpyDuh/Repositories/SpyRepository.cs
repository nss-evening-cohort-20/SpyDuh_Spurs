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
S.Name, S.UserName, S.Email, S.IsMember, S.DateCreated,

E.Id, E.SpyId, E.EnemySpyId,

ES.Id AS EnemyId, ES.Name AS EnemyName, ES.UserName AS EnemyUserName, ES.Email AS EnemyEmail, ES.IsMember AS EnemyIsMember, ES.DateCreated AS EnemyDateCreated,

SJ.skillLevel,

SK.Id AS SkillId, SK.skillName,

SRVJ.Cost,

SRV.Id AS ServiceId, SRV.serviceName

FROM Spy S

LEFT JOIN Enemy E 
ON E.spyId = S.id

LEFT JOIN Spy ES
ON E.EnemySpyId = ES.id

LEFT JOIN SKillJoin SJ
ON SJ.SpyId = ES.id

LEFT JOIN
Skill SK
ON SK.id = SJ.SkillId

LEFT JOIN [ServiceJoin] SRVJ
ON SRVJ.id = ES.id

LEFT JOIN [Service] SRV
ON SRV.id = SRVJ.serviceId

WHERE S.Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    var reader = cmd.ExecuteReader();
                    var spy = new Spy();
                    var enemies = new List<Spy>();
                    while (reader.Read())
                    {

                        var enemyId = DbUtils.GetInt(reader, "EnemyId");
                        var existingEnemy = enemies.FirstOrDefault(x => x.Id == enemyId);

                        if(existingEnemy == null)
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
                                Services= new List<Service>()
                            };

                            enemies.Add(existingEnemy);

                        }

                        existingEnemy.Skills.Add(new Skill()
                        {
                            Id = DbUtils.GetInt(reader, "SkillId"),
                            SkillName = DbUtils.GetString(reader,"skillName"),
                            SkillLevel = DbUtils.GetInt(reader,"skillLevel")
                        });

                        existingEnemy.Services.Add(new Service()
                        {
                            Id = DbUtils.GetInt(reader, "ServiceId"),
                            ServiceName = DbUtils.GetString(reader,"serviceName"),
                            Cost = DbUtils.GetInt(reader,"cost")
                        });


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

        public Spy getFriends(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT
                    S.Name, S.UserName, S.Email, S.IsMember, S.DateCreated,
                    F.Id, F.SpyId, F.friendSpyId,
                    FS.Id AS FriendId, FS.Name AS FriendName, FS.UserName AS FriendUserName, FS.Email AS FriendEmail, FS.IsMember AS FriendIsMember, FS.DateCreated AS FriendDateCreated

                    FROM Spy S
                    LEFT JOIN Friends F 
                    ON F.spyId = S.Id
                    LEFT JOIN Spy FS 
                    ON F.friendSpyId = FS.id
                    WHERE S.Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();
                    var spy = new Spy();
                    var friends = new List<Spy>();
                    while (reader.Read())

                    {
                        var friendId = DbUtils.GetInt(reader, "FriendId");
                        var exisitingFriend = friends.FirstOrDefault(x => x.Id == friendId);
                        if (exisitingFriend == null)
                        {
                            exisitingFriend = new Spy()
                            {
                                Id = friendId,
                                Name = DbUtils.GetString(reader, "FriendName"),
                                UserName = DbUtils.GetString(reader, "FriendUserName"),
                                Email = DbUtils.GetString(reader, "FriendEmail"),
                                IsMemeber = DbUtils.GetBoolean(reader, "FriendIsMember"),
                                DateCreated = DbUtils.GetDateTime(reader, "FriendDateCreated")

                            };

                            friends.Add(exisitingFriend);
                        }
                       
                       
                           
                        spy = new Spy
                        {
                            Id = id,
                            Name = DbUtils.GetString(reader, "Name"),
                            UserName = DbUtils.GetString(reader, "UserName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            IsMemeber = DbUtils.GetBoolean(reader, "IsMember"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            Friends = friends

                        };

                        
                    }

                    reader.Close();
                    return spy;
                }
            }
        }
    }
}
