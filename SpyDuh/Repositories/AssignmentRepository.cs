using SpyDuh.Models;
using SpyDuh.Utils;
using System.Reflection.Metadata.Ecma335;

namespace SpyDuh.Repositories
{
    public class AssignmentRepository : BaseRepository, IAssignmentRepository
    {
        public AssignmentRepository(IConfiguration configuration) : base(configuration) { }

        public Assignment GetAssignment(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    var sqlQuery =
                        @"SELECT 
                        A.id as AssignmentId, A.name as AssignmentName, A.handlerId, A.spyId, A.allotedTime, A.dateCreated, A.endDate, A.status,
                        DATEDIFF(DAY, CURRENT_TIMESTAMP, A.endDate) as days,
                        S.name as SpyName, H.name as HandlerName
                        FROM Assignment A
                        JOIN Handler H
                        ON A.handlerId = H.id
                        JOIN Spy S
                        ON A.spyId = S.id
                        WHERE A.id = @id";

                    cmd.CommandText = sqlQuery;

                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();

                    Assignment assignment = null;
                    if (reader.Read())
                    {
                        assignment = new Assignment()
                        {
                            Id = id,
                            Name = DbUtils.GetString(reader, "AssignmentName"),
                            Handler = new AssignmentHandler()
                            {
                                Id = DbUtils.GetInt(reader, "handlerId"),
                                Name = DbUtils.GetString(reader, "HandlerName")
                            },
                            Spy = new AssigmentSpy()
                            {
                                Id = DbUtils.GetInt(reader, "spyId"),
                                Name = DbUtils.GetString(reader, "SpyName")
                            },
                            AllotedTime = DbUtils.GetInt(reader, "allotedTime"),
                            DateCreated = DbUtils.GetDateTime(reader, "dateCreated"),
                            endDate = DbUtils.GetDateTime(reader, "endDate"),
                            daysRemaining = DbUtils.GetInt(reader, "days"),
                            Status = DbUtils.GetString(reader, "status")
                        };
                    }

                    reader.Close();
                    return assignment;
                }
            }
        }
    }
}
