using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;

namespace SpyDuh.Utils
{
    public static class DbUtils
    {
        public static string GetString(SqlDataReader reader, string column)
        {
            return reader.GetString(reader.GetOrdinal(column));
        }

        public static int GetInt(SqlDataReader reader, string column)
        {
            return reader.GetInt32(reader.GetOrdinal(column));  
        }
        public static DateTime GetDateTime(SqlDataReader reader, string column)
        {
            return reader.GetDateTime(reader.GetOrdinal(column));
        }
        public static void AddParameter(SqlCommand cmd, string name, object value)
        {
            if(value == null)
            {
                cmd.Parameters.AddWithValue(name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(name, value);
            }
        }

        public static bool GetBoolean(SqlDataReader reader, string column)
        {
            return reader.GetBoolean(reader.GetOrdinal(column));
        }

    }
}
