using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Boggle
{
    public class SQLUtils
    {

        public static SqlTransaction BeginTransaction(string connectionString, out SqlConnection connection)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            connection = conn;
            return conn.BeginTransaction();
        }

        public static T ExecuteQuery<T>(SqlConnection conn, SqlTransaction transaction, SqlCommand command, Func<SqlDataReader, T> convert)
        {
            T t;
            using (conn)
            {
                using (transaction)
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        t = convert(reader);
                    }
                    transaction.Commit();
                }
            }
            return t;
        }

        public static void ExecuteNonQuery(SqlConnection conn, SqlTransaction transaction, SqlCommand command, Action<int> callback)
        {
            using (conn)
            {
                using (transaction)
                {
                    int affected = command.ExecuteNonQuery();
                    callback(affected);
                    transaction.Commit();
                }
            }
        }

        public static void AddWithValue(SqlCommand command, IDictionary<string, string> mappings)
        {
            foreach(KeyValuePair<string,string> pair in mappings)
            {
                command.Parameters.AddWithValue(pair.Key, pair.Value);
            }
        }

        public static IDictionary<string,string> BuildMappings(params string[] mappings)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            if (mappings.Length % 2 != 0)
            {
                throw new ArgumentException("Mappings must be matched pairs");
            }
            for(int i = 0; i < mappings.Length-1; i+=2)
            {
                dic.Add(mappings[i], mappings[i + 1]);
            }
            return dic;
        }

        public static bool TableContains(string connectionString, string tableName, string columnName, string item)
        {
            SqlConnection conn;
            SqlTransaction trans = BeginTransaction(connectionString, out conn);
            SqlCommand command = new SqlCommand(String.Format("select * from {0} where @column = @item", tableName), conn, trans);
            AddWithValue(command, BuildMappings("@column", columnName, "@item", item));
            return ExecuteQuery<bool>(conn, trans, command, (r)=> {
                return r.HasRows;
            });
        }

        public static bool DoIfContains(string connectionString, string tableName, string columnName, string item, Action<SqlDataReader> ifContained)
        {
            SqlConnection conn;
            SqlTransaction trans = BeginTransaction(connectionString, out conn);
            SqlCommand command = new SqlCommand(String.Format("select * from {0} where @column = @item", tableName), conn, trans);
            AddWithValue(command, BuildMappings("@column", columnName, "@item", item));
            return ExecuteQuery<bool>(conn, trans, command, (r) => {
                if (r.HasRows)
                {
                    ifContained(r);
                    return true;
                } else
                {
                    return false;
                }
            });
        }
    }
}