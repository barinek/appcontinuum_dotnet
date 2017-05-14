using System.IO;
using System.Reflection;

namespace TestSupport
{
    public class TestScenarioSupport
    {
        private readonly TestDataSourceConfig _dataSourceConfig = new TestDataSourceConfig();

        public void LoadTestScenario(string scenario)
        {
            var assembly = Assembly.Load(new AssemblyName("TestSupport"));
            var manifestResourceStream = assembly.GetManifestResourceStream($"TestSupport.scenarios.{scenario}.sql");
            var sql = new StreamReader(manifestResourceStream).ReadToEnd();

            var connection = _dataSourceConfig.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}