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
                    //transaction.Commit();
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

        public static void AddWithValue(SqlCommand command, IDictionary<string, object> mappings)
        {
            foreach(KeyValuePair<string, object> pair in mappings)
            {
                command.Parameters.AddWithValue(pair.Key, pair.Value);
            }
        }

        public static IDictionary<string, object> BuildMappings(params object[] mappings)
        {
            IDictionary<string, object> dic = new Dictionary<string, object>();
            if (mappings.Length % 2 != 0)
            {
                throw new ArgumentException("Mappings must be matched pairs");
            }
            for(int i = 0; i < mappings.Length-1; i+=2)
            {
                dic.Add((string) mappings[i], mappings[i + 1]);
            }
            return dic;
        }

        public static bool TableContains(string connectionString, string tableName, string columnName, string item)
        {
            SqlConnection conn;
            SqlTransaction trans = BeginTransaction(connectionString, out conn);
            SqlCommand command = new SqlCommand("select * from "+ tableName +" where "+columnName+" = "+item, conn, trans);
            Console.WriteLine(command.CommandText);
            return ExecuteQuery<bool>(conn, trans, command, (r)=> {
                return r.HasRows;
            });
        }

        public static bool DoIfContains(string connectionString, string tableName, string columnName, string item, Action<SqlDataReader> ifContained)
        {
            SqlConnection conn;
            SqlTransaction trans = BeginTransaction(connectionString, out conn);
            SqlCommand command = new SqlCommand("select * from " + tableName + " where " + columnName + " = " + item, conn, trans);
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