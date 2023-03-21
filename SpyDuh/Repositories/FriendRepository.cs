﻿using SpyDuh.Models;
using SpyDuh.Utils;

namespace SpyDuh.Repositories
{
    public class FriendRepository : BaseRepository, IFriendRepository
    {
        public FriendRepository(IConfiguration configuration) : base(configuration) { }
        public Spy GetFriends(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT
                    S.id as sId, S.Name, S.UserName, S.Email, S.IsMember, S.DateCreated,
                    F.Id, F.SpyId, F.friendId,
                    FS.Id AS FriendId, FS.Name AS FriendName, FS.UserName AS FriendUserName, FS.Email AS FriendEmail, FS.IsMember AS FriendIsMember, FS.DateCreated AS FriendDateCreated
                    FROM Spy S
                    LEFT JOIN Friends F
                    ON F.spyId = S.Id
                    LEFT JOIN Spy FS
                    ON F.friendId = FS.id
                    WHERE S.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();
                    Spy friend = null;
                    while (reader.Read())
                    {
                        var friendId = DbUtils.GetInt(reader, "sId");
                        //var existingFriend = spyFriends.FirstOrDefault(x => x.Id == friendId);
                        if (friendId == null)
                        {
                            friend = new Spy()
                            {
                                Id = friendId,
                                Name = DbUtils.GetString(reader, "name"),
                                UserName = DbUtils.GetString(reader, "userName"),
                                Email = DbUtils.GetString(reader, "email"),
                                IsMemeber = DbUtils.GetBool(reader, "isMember"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                Friends = new List<Friend>()
                            };
                        }
                        

                            //var newId = DbUtils.GetInt(reader, "FriendId");
                            //var newFriendId = spyFriends.FirstOrDefault(x => x.Id == friendId);

                            //friend.Friends.Add(new Friend()
                            //{
                            //    Id = DbUtils.GetInt(reader, "FriendId"),
                            //    spy = new Spy()
                            //    {
                            //        Id = DbUtils.GetInt(reader, "FriendId"),
                            //        Name = DbUtils.GetString(reader, "FriendName"),
                            //    }
                            //});
                    }
                    reader.Close();

                    return friend;
                }
            }
        }
    }
}
