using SpyDuh.Models;
using SpyDuh.Utils;

namespace SpyDuh.Repositories
{
    public class HandlerRepository : BaseRepository, IHandlerRepository
    {

        public HandlerRepository(IConfiguration configuration) : base(configuration) { }

        public Agency listSpies(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
SELECT
Agency.[name] AS AgencyName,
S.id AS SpyId, S.[name] AS SpyName, S.userName, S.email, s.DateCreated AS SpyDateCreated,
A.id As AssignmentId, A.[name] AS AssignmentName, DATEDIFF(DAY, CURRENT_TIMESTAMP, A.endDate) AS DaysRemaining, A.isCompleted,
SJ.id AS SkillJoinId,
SRVJ.id AS ServiceJoinId,
Skill.id AS SkillId,
Skill.skillName,
SJ.skillLevel,
service.id AS ServiceId,
service.serviceName,
SRVJ.Cost

FROM Handler H

LEFT JOIN Agency
ON Agency.id = H.agencyId

LEFT JOIN Spy S
ON S.handlerId = H.id

LEFT JOIN Assignment A
ON A.spyId = S.id

LEFT JOIN SkillJoin SJ
ON SJ.spyId = S.id

LEFT JOIN Skill
ON Skill.id = SJ.skillId

LEFT JOIN ServiceJoin SRVJ
ON SRVJ.spyId = S.id

LEFT JOIN Service
ON Service.id = SRVJ.serviceId

WHERE H.id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    var reader = cmd.ExecuteReader();
                    var agency = new Agency();
                    var spies = new List<HandlerSpy>();
                    while (reader.Read())
                    {
                        var spyId = DbUtils.GetInt(reader, "SpyId");
                        var existingSpy = spies.FirstOrDefault(x => x.Id == spyId);

                        if (existingSpy == null)
                        {
                            existingSpy = new HandlerSpy()
                            {
                                Id = spyId,
                                Name = DbUtils.GetString(reader, "SpyName"),
                                UserName = DbUtils.GetString(reader, "userName"),
                                Email = DbUtils.GetString(reader, "email"),
                                DateCreated = DbUtils.GetDateTime(reader, "SpyDateCreated"),
                                Skills = new List<Skill>(),
                                Services = new List<Service>(),
                                Assignments = new List<AssignmentShort>()
                            };

                            spies.Add(existingSpy);
                        }

                        if(DbUtils.IsNotDbNull(reader, "SkillId"))
                        {
                            var skillId = DbUtils.GetInt(reader, "SkillId");
                            var existingSkill = existingSpy.Skills.FirstOrDefault(x => x.Id == skillId);


                            if (existingSkill == null)
                            {
                                existingSkill = new Skill()
                                {
                                    Id = skillId,
                                    SkillName = DbUtils.GetString(reader, "skillName"),
                                    SkillLevel = DbUtils.GetInt(reader, "skillLevel")
                                };
                                existingSpy.Skills.Add(existingSkill);
                            }

                        }
                   
                            
                        
                        if(DbUtils.IsNotDbNull(reader, "ServiceId"))
                        {
                            var serviceId = DbUtils.GetInt(reader, "ServiceId");
                            var existingService = existingSpy.Services.FirstOrDefault(x => x.Id == serviceId);



                            if (existingService == null)
                            {
                                existingService = new Service()
                                {
                                    Id = serviceId,
                                    ServiceName = DbUtils.GetString(reader, "serviceName"),
                                    Cost = DbUtils.GetInt(reader, "cost")
                                };
                                existingSpy.Services.Add(existingService);
                            }
                        }


                        if (DbUtils.IsNotDbNull(reader, "AssignmentId"))
                        {
                            var assignmentId = DbUtils.GetInt(reader, "AssignmentId");
                            var existingAssignment = existingSpy.Assignments.FirstOrDefault(x => x.Id == assignmentId);

                            if (DbUtils.IsNotDbNull(reader, "AssignmentId"))
                            {

                                existingAssignment = new AssignmentShort()
                                {
                                    Id = assignmentId,
                                    Name = DbUtils.GetString(reader, "AssignmentName"),
                                    IsCompleted = DbUtils.GetBoolean(reader, "isCompleted"),
                                    DaysRemaining = DbUtils.GetInt(reader, "DaysRemaining"),

                                };
                                existingSpy.Assignments.Add(existingAssignment);

                            }
                        }

                        agency = new Agency()
                        {
                            AgencyName = DbUtils.GetString(reader, "AgencyName"),
                            Spies = spies
                        };

                    }

                    reader.Close();

                    return agency;

                }
            }
        }


    }
}
