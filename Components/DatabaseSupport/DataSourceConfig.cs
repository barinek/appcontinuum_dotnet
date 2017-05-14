using System;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace DatabaseSupport
{
    public class DataSourceConfig : IDataSourceConfig
    {
        public DbConnection GetConnection()
        {
            var json = Environment.GetEnvironmentVariable("VCAP_SERVICES");
            var connection = ParseJson(json);
            return new MySqlConnection(connection); // todo - use db pool lib?
        }

        public string ParseJson(string json)
        {
            var services = JObject.Parse(json);
            var credentials = services["p-mysql"].First["credentials"];
            var host = credentials["hostname"];
            var port = credentials["port"];
            var database = credentials["name"];
            var user = credentials["username"];
            var password = credentials["password"];

            return $"server={host};user id={user};password={password};port={port};database={database};";
        }
    }
}