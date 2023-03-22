//using SpyDuh.Models;
//using SpyDuh.Utils;

//namespace SpyDuh.Repositories
//{
//    public class HandlerRepository: BaseRepository
//    {
    
//            public HandlerRepository(IConfiguration configuration) : base(configuration) { }

//        public Handler listSpies(int id)
//        {
//            using (var conn = Connection)
//            {
//                conn.Open();
//                using (var cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//SELECT
//H.[name] AS HandlerName,
//Agency.[name] AS AgencyName,
//S.id AS SpyId, S.[name] AS SpyName, S.userName, S.email, S.isMember, s.DateCreated AS SpyDateCreated,
//A.[name] AS AssignmentName, A.allotedTime, A.dateCreated AS AssignmentDateCreated, A.Status


//FROM Handler H

//LEFT JOIN Agency
//ON Agency.id = H.agencyId

//LEFT JOIN Spy S
//ON S.handlerId = H.id

//LEFT JOIN Assignment A
//ON A.spyId = S.id

//WHERE H.id = @id";

//                    DbUtils.AddParameter(cmd, "@id", id); 
//                    var reader = cmd.ExecuteReader();
//                    var handler = new Handler();
//                    var agency = new Agency();
//                    var spies = new List<Spy>();
//                    while (reader.Read())
//                    {
//                        var spyId = DbUtils.GetInt(reader, "SpyId");
//                        var existingSpy = spies.FirstOrDefault(x => x.Id == spyId);

//                        if(existingSpy == null)
//                        {
//                            existingSpy = new Spy()
//                            {
//                                Id = spyId,
//                                Name = DbUtils.GetString(reader, "SpyName"),
//                                UserName = DbUtils.GetString(reader, "userName"),
//                                Email = DbUtils.GetString(reader, "email"),
//                                IsMemeber = DbUtils.GetBool(reader, "isMember"),
//                                DateCreated = DbUtils.GetDateTime(reader, "SpyDateCreated")
//                            };

//                            spies.Add(existingSpy);

//                        }

//                        agency = new Agency()
//                        {
//                            Name = DbUtils.GetString(reader, "AgencyName")
//                        };

//                        handler = new Handler()
//                        {
//                            Id = id,
//                            Name = DbUtils.GetString(reader, "HandlerName"),

//                        };

//                    }

//                }
//            }
//        }

            
//    }
//}
