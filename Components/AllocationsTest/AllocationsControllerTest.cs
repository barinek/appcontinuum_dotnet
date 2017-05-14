using System;
using System.Collections.Generic;
using Allocations;
using DatabaseSupport;
using Microsoft.AspNetCore.Mvc;
using TestSupport;
using Xunit;

namespace AllocationsTest
{
    [Collection("Allocations")]
    public class AllocationsControllerTest
    {
        private readonly TestDataSourceConfig _dataSourceConfig = new TestDataSourceConfig();
        private readonly TestScenarioSupport _testScenarioSupport = new TestScenarioSupport();

        [Fact]
        public void TestPost()
        {
            var server = new TestServer<ProjectClientResponse>("http://localhost:3001", 500);
            server.Start();

            _testScenarioSupport.LoadTestScenario("jacks-test-scenario");

            var controller =
                new AllocationController(new AllocationDataGateway(new DatabaseTemplate(_dataSourceConfig)),
                    new ProjectClient("http://localhost:3001"));

            var value = controller.Post(new AllocationInfo(-1, 55432, 4765, DateTime.Parse("2014-05-16"),
                DateTime.Parse("2014-05-26"), ""));
            var actual = (AllocationInfo) ((ObjectResult) value).Value;

            Assert.True(actual.Id > 0);
            Assert.Equal(55432L, actual.ProjectId);
            Assert.Equal(4765L, actual.UserId);
            Assert.Equal(16, actual.FirstDay.Day);
            Assert.Equal(26, actual.LastDay.Day);
            Assert.Equal("allocation info", actual.Info);

            server.Stop();
        }

        [Fact]
        public void TestGet()
        {
            _testScenarioSupport.LoadTestScenario("jacks-test-scenario");

            var controller =
                new AllocationController(new AllocationDataGateway(new DatabaseTemplate(_dataSourceConfig)),
                    new ProjectClient());
            var result = controller.Get(55432);

            // todo...
            Assert.Equal(2, ((List<AllocationInfo>) ((ObjectResult) result).Value).Count);
        }
    }

    public class ProjectClientResponse : TestServerResponse
    {
        public override string Response()
        {
            return "{\"active\" : true }";
        }
    }
}