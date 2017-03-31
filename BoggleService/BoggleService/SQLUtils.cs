using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Boggle
{
    public class SQLUtils
    {

        public SqlTransaction BeginTransaction(string connectionString, out SqlConnection connection)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            connection = conn;
            return conn.BeginTransaction();
        }

        public void ExecuteNonQuery(SqlConnection conn, SqlTransaction transaction, SqlCommand command, Action<int> callback)
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
    }
}