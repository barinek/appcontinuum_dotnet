using DatabaseSupport;
using MySql.Data.MySqlClient;

namespace TestSupport
{
    public class TestDataSourceConfig : DataSourceConfig
    {
        private readonly MySqlConnection _connection;

        public TestDataSourceConfig()
        {
            _connection = (MySqlConnection) GetConnection();
            _connection.Open();
            var command = _connection.CreateCommand();
            command.CommandText =
                @"delete from allocations; delete from stories; delete from time_entries; delete from projects; delete from accounts; delete from users;";
            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}