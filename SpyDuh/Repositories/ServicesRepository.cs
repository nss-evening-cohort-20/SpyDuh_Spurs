using Microsoft.Extensions.Hosting;
using SpyDuh.Models;
using SpyDuh.Utils;

namespace SpyDuh.Repositories
{
    public class ServicesRepository : BaseRepository, IServicesRepository
    {
        public ServicesRepository(IConfiguration configuration) : base(configuration) { }

        public List<Service> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                  SELECT s.Id AS ServiceId, s.ServiceName, s.Cost,
                
                sp.Id AS SpyId, sp.Name, sp.UserName ,sp.Email, sp.DateCreated
                
                FROM Services s
                LEFT JOIN Spy sp ON s.SpyID = sp.Id";

                    var reader = cmd.ExecuteReader();

                    var services = new List<Service>();
                    while (reader.Read())
                    {
                        services.Add(new Service()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            ServiceName = DbUtils.GetString(reader, "ServiceName"),
                            Cost = DbUtils.GetInt(reader, "Cost"),
                            SpyId = DbUtils.GetInt(reader, "SpyId"),
                        });
                    }

                    reader.Close();

                    return services;
                }
            }
        }
        public Service GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT ServiceName, Cost, SpyID
                      FROM Service
                     WHERE Id = @Id
                      ";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Service service = null;
                    if (reader.Read())
                    {
                        service = new Service()
                        {
                            Id = id,
                            ServiceName = DbUtils.GetString(reader, "ServiceName"),
                            Cost = DbUtils.GetInt(reader, "Cost"),
                            SpyId = DbUtils.GetInt(reader, "SpyId"),
                        };
                    }

                    reader.Close();

                    return service;
                }
            }
        }
        public void Add(Service service)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                     INSERT INTO Services (ServiceName, Cost, SpyId)
                      OUTPUT INSERTED.ID
                    VALUES (@ServiceName, @Cost, @SpyId)";

                    DbUtils.AddParameter(cmd, "@Service", service.ServiceName);
                    DbUtils.AddParameter(cmd, "@Cost", service.Cost);
                    DbUtils.AddParameter(cmd, "@SpyId", service.SpyId);

                    service.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
