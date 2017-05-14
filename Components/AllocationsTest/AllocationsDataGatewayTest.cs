using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Allocations;
using DatabaseSupport;
using TestSupport;
using Xunit;

namespace AllocationsTest
{
    [Collection("Allocations")]
    public class AllocationDataGatewayTest
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

            var gateway = new AllocationDataGateway(new DatabaseTemplate(_dataSourceConfig));
            gateway.Create(22, 12, DateTime.Now, DateTime.Now);

            // todo...
            var template = new DatabaseTemplate(_dataSourceConfig);
            var projectIds = template.Query("select project_id from allocations", reader => reader.GetInt64(0),
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
insert into allocations (id, project_id, user_id, first_day, last_day) values (97336, 22, 12, now(), now());
");

            var gateway = new AllocationDataGateway(new DatabaseTemplate(_dataSourceConfig));
            var list = gateway.FindBy(22);

            // todo...
            var actual = list.First();
            Assert.Equal(97336, actual.Id);
            Assert.Equal(22, actual.ProjectId);
            Assert.Equal(12, actual.UserId);
        }
    }
}