using Microsoft.Extensions.Hosting;
using SpyDuh.Models;
using SpyDuh.Utils;

namespace SpyDuh.Repositories
{
    public class ServicesRepository : BaseRepository, IServicesRepository
    {
        public ServicesRepository(IConfiguration configuration) : base(configuration) { }

        public List<Service> GetAll(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT s.Id AS ServiceId, s.ServiceName, sj.Cost, spy.UserName, spy.Name
                    FROM Service s
                    LEFT JOIN ServiceJoin sj
                    ON s.Id = sj.serviceId
                    LEFT JOIN Spy spy
                    ON spy.id = sj.id
                    WHERE spy.id = @id
                     ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();

                    var service = new List<Service>();
                    while (reader.Read())
                    {
                        service.Add(new Service()
                        {
                            Id = DbUtils.GetInt(reader, "ServiceId"),
                            ServiceName = DbUtils.GetString(reader, "ServiceName"),
                            Cost = DbUtils.GetInt(reader, "Cost"),
                            Spy = new Spy()
                            {
                                UserName = DbUtils.GetString(reader, "UserName"),
                                Name = DbUtils.GetString(reader, "Name")
                            }
                        });
                    }
                    
                    reader.Close();

                    return service;
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
                    SELECT serviceName
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
                            ServiceName = DbUtils.GetString(reader, "serviceName")
                        };
                    }

                    reader.Close();

                    return service;
                }
            }
        }
        public void Add(ServiceWithoutCost service)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                     INSERT INTO Service (serviceName)
                      OUTPUT INSERTED.ID
                    VALUES (@serviceName)";

                    DbUtils.AddParameter(cmd, "@serviceName", service.ServiceName);

                    service.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void Update(ServiceWithoutCost service)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                     UPDATE Service
                     SET serviceName = @serviceName
                     WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@serviceName", service.ServiceName);
                    DbUtils.AddParameter(cmd, "@Id", service.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id) 
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Service WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
