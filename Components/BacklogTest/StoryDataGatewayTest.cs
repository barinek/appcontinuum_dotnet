using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Backlog;
using DatabaseSupport;
using TestSupport;
using Xunit;

namespace BacklogTest
{
    [Collection("Backlog")]
    public class StoryDataGatewayTest
    {
        private readonly TestDataSourceConfig _dataSourceConfig = new TestDataSourceConfig();

        [Fact]
        public void TestCreate()
        {
            var support = new TestDatabaseSupport(_dataSourceConfig);
            support.ExecSql(@"
insert into users (id, name) values (12, 'Jack');
insert into accounts (id, owner_id, name) values (1, 12, 'anAccount');
insert into projects (id, account_id, name) values (22, 1, 'aProject');
");

            var gateway = new StoryDataGateway(new DatabaseTemplate(_dataSourceConfig));
            gateway.Create(22, "aStory");

            // todo...
            var template = new DatabaseTemplate(_dataSourceConfig);
            var projectIds = template.Query("select project_id from stories", reader => reader.GetInt64(0),
                new List<DbParameter>());

            Assert.Equal(22, projectIds.First());
        }

        [Fact]
        public void TestFind()
        {
            var support = new TestDatabaseSupport(_dataSourceConfig);
            support.ExecSql(@"
insert into users (id, name) values (12, 'Jack');
insert into accounts (id, owner_id, name) values (1, 12, 'anAccount');
insert into projects (id, account_id, name) values (22, 1, 'aProject');
insert into stories (id, project_id, name) values (1346, 22, 'aStory');
");

            var gateway = new StoryDataGateway(new DatabaseTemplate(_dataSourceConfig));
            var list = gateway.FindBy(22);

            // todo...
            var actual = list.First();
            Assert.Equal(1346, actual.Id);
            Assert.Equal(22, actual.ProjectId);
            Assert.Equal("aStory", actual.Name);
        }
    }
}