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
ES.Id AS EnemyId, ES.Name AS EnemyName, ES.UserName AS EnemyUserName, ES.Email AS EnemyEmail, ES.IsMember AS EnemyIsMember, ES.DateCreated AS EnemyDateCreated
FROM Spy S
LEFT JOIN Enemy E 
ON E.spyId = S.id
LEFT JOIN Spy ES
ON E.EnemySpyId = ES.id
WHERE S.Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    var reader = cmd.ExecuteReader();
                    var spy = new Spy();
                    var enemies = new List<Spy>();
                    while (reader.Read())
                    {



                        enemies.Add(new Spy
                        {
                            Id = DbUtils.GetInt(reader, "EnemyId"),
                            Name = DbUtils.GetString(reader, "EnemyName"),
                            UserName = DbUtils.GetString(reader, "EnemyUserName"),
                            Email = DbUtils.GetString(reader, "EnemyEmail"),
                            IsMemeber = DbUtils.GetBoolean(reader, "EnemyIsMember"),
                            DateCreated = DbUtils.GetDateTime(reader, "EnemyDateCreated")

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
                    FS.Id AS FriendId, FS.Email AS FriendEmail, FS.IsMember AS FriendIsMember, FS.DateCreated AS FriendDateCreated

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
                        friends.Add(new Spy
                        {
                            Id = DbUtils.GetInt(reader, "FriendId"),
                            Name = DbUtils.GetString(reader, "FriendName"),
                            UserName = DbUtils.GetString(reader, "FriendUserName"),
                            Email = DbUtils.GetString(reader, "FriendEmail"),
                            IsMemeber = DbUtils.GetBoolean(reader, "FriendIsMember"),
                            DateCreated = DbUtils.GetDateTime(reader, "FriendDateCreated")
                        });

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
