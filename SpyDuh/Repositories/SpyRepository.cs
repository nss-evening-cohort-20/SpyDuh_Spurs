using SpyDuh.Models;
using SpyDuh.Utils;

namespace SpyDuh.Repositories
{
    public class SpyRepository : BaseRepository, ISpyRepository
    {
        public SpyRepository(IConfiguration configuration) : base(configuration) { }

        public List<Spy> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT id, name, userName, email, isMember, DateCreated
                    FROM Spy
                    ORDER BY id";

                    var reader = cmd.ExecuteReader();

                    var spies = new List<Spy>();
                    while (reader.Read())
                    {
                        spies.Add(new Spy()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            Name = DbUtils.GetString(reader, "name"),
                            UserName = DbUtils.GetString(reader, "userName"),
                            Email = DbUtils.GetString(reader, "email"),
                            IsMember = DbUtils.GetBool(reader, "isMember"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                        });
                    }
                    reader.Close();

                    return spies;
                }
            }
        }

        public void Add(Spy spy)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Spy (name, userName, email, isMember, DateCreated)
                        OUTPUT INSERTED.ID
                        VALUES (@name, @userName, @email, @isMember, @DateCreated)";

                    DbUtils.AddParameter(cmd, "@name", spy.Name);
                    DbUtils.AddParameter(cmd, "@userName", spy.UserName);
                    DbUtils.AddParameter(cmd, "@email", spy.Email);
                    DbUtils.AddParameter(cmd, "@isMember", spy.IsMember);
                    DbUtils.AddParameter(cmd, "@DateCreated", spy.DateCreated);

                    spy.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
