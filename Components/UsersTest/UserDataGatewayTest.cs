using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using DatabaseSupport;
using TestSupport;
using Users;
using Xunit;

namespace UsersTest
{
    [Collection("Users")]
    public class UsersDataGatewayTest
    {
        private readonly TestDataSourceConfig _dataSourceConfig = new TestDataSourceConfig();

        [Fact]
        public void TestCreate()
        {
            var template = new DatabaseTemplate(_dataSourceConfig);
            var gateway = new UserDataGateway(template);
            gateway.Create("aUser");

            var names = template.Query("select name from users", reader => reader.GetString(0),
                new List<DbParameter>());

            Assert.Equal("aUser", names.First());
        }

        [Fact]
        public void TestFindBy()
        {
            var support = new TestDatabaseSupport(_dataSourceConfig);
            support.ExecSql(
                @"insert into users (id, name) values (42346, 'aName'), (42347, 'anotherName'), (42348, 'andAnotherName');");

            var gateway = new UserDataGateway(new DatabaseTemplate(_dataSourceConfig));

            var actual = gateway.FindObjectBy(42347);

            Assert.Equal(42347, actual.Id);
            Assert.Equal("anotherName", actual.Name);

            Assert.Null(gateway.FindObjectBy(42));
        }
    }
}