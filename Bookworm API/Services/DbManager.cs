using System;
using System.Data;
using System.Data.SqlClient;

namespace Bookworm_API.Services
{
    public class DbManager
    {

        private static SqlConnection _connection;
        private static readonly string ConnectionString = $"Data Source={Environment.MachineName};" +
                                                          $"Integrated Security=true;" +
                                                          $"Initial Catalog=TCCF";

        private static DbManager _currentInstance;
        public static DbManager CurrentInstance => _currentInstance ?? (_currentInstance = new DbManager());

        public DbManager()
        {
            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
        }

        public int ExecuteNonQuery(SqlCommand command)
        {
            command.Connection = _connection;
            return command.ExecuteNonQuery();
        }

        public DataTable Execute(SqlCommand command)
        {
            command.Connection = _connection;
            var dr = command.ExecuteReader();
            var dt = new DataTable();
            dt.Load(dr);
            return dt;
        }

        public object ExecuteScalar(SqlCommand command)
        {
            command.Connection = _connection;
            return command.ExecuteScalar();
        }
    }
}