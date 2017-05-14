using DatabaseSupport;
using Xunit;

namespace DatabaseSupportTest
{
    public class DataSourceConfigTest
    {
        [Fact]
        public void ParseJson()
        {
            var dataSourceConfig = new DataSourceConfig();

            var name = dataSourceConfig.ParseJson("{ \"p-mysql\": [ { \"credentials\": { \"hostname\": \"localhost\", \"port\": 3306, \"name\": \"uservices_test\", \"username\": \"root\", \"password\": \"\" } } ] }");
            Assert.Equal("server=localhost;user id=root;password=;port=3306;database=uservices_test;", name);

            var address = dataSourceConfig.ParseJson("{ \"p-mysql\": [ { \"credentials\": { \"hostname\": \"127.0.0.1\", \"port\": 3306, \"name\": \"uservices_test\", \"username\": \"root\", \"password\": \"\" } } ] }");
            Assert.Equal("server=127.0.0.1;user id=root;password=;port=3306;database=uservices_test;", address);
        }
    }
}