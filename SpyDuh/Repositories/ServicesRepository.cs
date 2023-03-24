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
                    SELECT 
                    s.serviceName,
                    sj.cost,
                    spy.name, spy.userName
                    FROM Service s
                    JOIN ServiceJoin sj
                    ON s.id = sj.serviceId
                    JOIN Spy spy
                    ON spy.id = sj.spyId ";

                    var reader = cmd.ExecuteReader();

                    var service = new List<Service>();
                    while (reader.Read())
                    {
                        service.Add(new Service()
                        {
                            //Id = DbUtils.GetInt(reader, "id"),
                            ServiceName = DbUtils.GetString(reader, "serviceName"),
                            //ServiceJoin = new ServiceJoin()
                            //{
                            //    cost = DbUtils.GetInt(reader, "cost")
                            //},
                            //Spy = new Spy()
                            //{
                            //    Name = DbUtils.GetString(reader, "name"),
                            //    UserName = DbUtils.GetString(reader, "userName")
                            //} 
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
        public void Add(Service service)
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
        public void Update(Service service)
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
