namespace TestSupport
{
    public class TestDatabaseSupport
    {
        private readonly TestDataSourceConfig _dataSourceConfig;

        public TestDatabaseSupport(TestDataSourceConfig dataSourceConfig)
        {
            _dataSourceConfig = dataSourceConfig;
        }

        public void ExecSql(string sql)
        {
            var connection = _dataSourceConfig.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}