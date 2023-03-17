using SpyDuh.Models;
using SpyDuh.Utils;

namespace SpyDuh.Repositories
{
    public class SpyRepository : BaseRepository, ISpyRepository
    {
        public SpyRepository(IConfiguration configuration) : base(configuration) { }

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
