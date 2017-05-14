using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Accounts;
using DatabaseSupport;
using TestSupport;
using Xunit;

namespace AccountsTest
{
    [Collection("Accounts")]
    public class AccountDataGatewayTest
    {
        private readonly TestDataSourceConfig _dataSourceConfig = new TestDataSourceConfig();

        [Fact]
        public void TestCreate()
        {
            var support = new TestDatabaseSupport(_dataSourceConfig);
            support.ExecSql(@"
insert into users (id, name) values (12, 'Jack');
");

            var template = new DatabaseTemplate(_dataSourceConfig);
            var gateway = new AccountDataGateway(template);
            gateway.Create(12, "anAccount");

            var names = template.Query("select name from accounts", reader => reader.GetString(0),
                new List<DbParameter>());

            Assert.Equal("anAccount", names.First());
        }

        [Fact]
        public void TestFindBy()
        {
            var support = new TestDatabaseSupport(_dataSourceConfig);
            support.ExecSql(@"
insert into users (id, name) values (12, 'Jack');
insert into accounts (id, owner_id, name) values (1, 12, 'anAccount');
");

            var gateway = new AccountDataGateway(new DatabaseTemplate(_dataSourceConfig));

            var actual = gateway.FindBy(12);

            Assert.Equal(1, actual.Id);
            Assert.Equal(12, actual.OwnerId);
            Assert.Equal("anAccount", actual.Name);
        }
    }
}