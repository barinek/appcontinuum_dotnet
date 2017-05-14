using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using DatabaseSupport;
using Projects;
using TestSupport;
using Xunit;

namespace ProjectsTest
{
    [Collection("Projects")]
    public class ProjectDataGatewayTest
    {
        private readonly TestDataSourceConfig _dataSourceConfig = new TestDataSourceConfig();

        [Fact]
        public void TestCreate()
        {
            var support = new TestDatabaseSupport(_dataSourceConfig);
            support.ExecSql(@"
insert into users (id, name) values (12, 'Jack');
insert into accounts (id, owner_id, name) values (1, 12, 'anAccount');
");

            var gateway = new ProjectDataGateway(new DatabaseTemplate(_dataSourceConfig));
            gateway.Create(1, "aProject");

            // todo...
            var template = new DatabaseTemplate(_dataSourceConfig);
            var projects = template.Query("select name from projects where account_id = 1",
                reader => reader.GetString(0),
                new List<DbParameter>());

            Assert.Equal("aProject", projects.First());
        }

        [Fact]
        public void TestFind()
        {
            var support = new TestDatabaseSupport(_dataSourceConfig);
            support.ExecSql(@"
insert into users (id, name) values (12, 'Jack');
insert into accounts (id, owner_id, name) values (1, 12, 'anAccount');
insert into projects (id, account_id, name) values (22, 1, 'aProject');
");

            var gateway = new ProjectDataGateway(new DatabaseTemplate(_dataSourceConfig));
            var list = gateway.FindBy(1);

            // todo...
            var actual = list.First();
            Assert.Equal(22, actual.Id);
            Assert.Equal(1, actual.AccountId);
            Assert.Equal("aProject", actual.Name);
            Assert.Equal(true, actual.Active);
        }

        [Fact]
        public void TestFindObject()
        {
            var support = new TestDatabaseSupport(_dataSourceConfig);
            support.ExecSql(@"
insert into users (id, name) values (12, 'Jack');
insert into accounts (id, owner_id, name) values (1, 12, 'anAccount');
insert into projects (id, account_id, name, active) values (22, 1, 'aProject', true);
");

            var gateway = new ProjectDataGateway(new DatabaseTemplate(_dataSourceConfig));
            var actual = gateway.FindObject(22);

            // todo...
            Assert.Equal(22, actual.Id);
            Assert.Equal(1, actual.AccountId);
            Assert.Equal("aProject", actual.Name);
            Assert.Equal(true, actual.Active);
        }
    }
}