using SpyDuh.Models;
using SpyDuh.Utils;

namespace SpyDuh.Repositories
{
    public class FriendRepository : BaseRepository, IFriendRepository
    {
        public FriendRepository(IConfiguration configuration) : base(configuration) { }
        public Spy GetFriends(int spyId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT
                    S.id as sId, S.Name, S.UserName, S.Email, S.IsMember, S.DateCreated,
                    F.Id, F.SpyId as fId, F.friendId,
                    FS.Id AS FriendId, FS.Name AS FriendName, FS.UserName AS FriendUserName, FS.Email AS FriendEmail, FS.IsMember AS FriendIsMember, FS.DateCreated AS FriendDateCreated
                    FROM Spy S
                    LEFT JOIN Friends F
                    ON F.spyId = S.Id
                    LEFT JOIN Spy FS
                    ON F.friendId = FS.id
                    WHERE S.Id = @id";

                    cmd.Parameters.AddWithValue("@id", spyId);

                    var reader = cmd.ExecuteReader();
                    Spy spy = null;

                    while (reader.Read())
                    {
                        if (spy == null)
                        {
                            spy = new Spy()
                            {
                                Id = DbUtils.GetInt(reader, "sId"),
                                Name = DbUtils.GetString(reader, "name"),
                                UserName = DbUtils.GetString(reader, "userName"),
                                Email = DbUtils.GetString(reader, "email"),
                                IsMemeber = DbUtils.GetBool(reader, "isMember"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                Friends = new List<Friend>()

                            };
                        }

                        spy.Friends.Add(new Friend()
                        {                            
                            spy = new Spy()
                            {
                                Id = DbUtils.GetInt(reader, "FriendId"),
                                Name = DbUtils.GetString(reader, "FriendName"),
                            }
                        });
                    }

                    reader.Close();

                    return spy;
                }
            }
        }
    }
}
