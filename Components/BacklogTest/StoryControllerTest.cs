using System.Collections.Generic;
using Backlog;
using DatabaseSupport;
using Microsoft.AspNetCore.Mvc;
using TestSupport;
using Xunit;

namespace BacklogTest
{
    [Collection("Backlog")]
    public class StoryControllerTest
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
                new StoryController(new StoryDataGateway(new DatabaseTemplate(_dataSourceConfig)),
                    new ProjectClient("http://localhost:3001"));

            var value = controller.Post(new StoryInfo(-1, 55432, "An epic story", ""));
            var actual = (StoryInfo) ((ObjectResult) value).Value;

            Assert.True(actual.Id > 0);
            Assert.Equal(55432, actual.ProjectId);
            Assert.Equal("An epic story", actual.Name);
            Assert.Equal("story info", actual.Info);

            server.Stop();
        }

        [Fact]
        public void TestGet()
        {
            _testScenarioSupport.LoadTestScenario("jacks-test-scenario");

            var controller =
                new StoryController(new StoryDataGateway(new DatabaseTemplate(_dataSourceConfig)),
                    new ProjectClient("http://localhost:3001"));
            var result = controller.Get(55432);

            // todo...
            Assert.Equal(2, ((List<StoryInfo>) ((ObjectResult) result).Value).Count);
        }

        public class ProjectClientResponse : TestServerResponse
        {
            public override string Response()
            {
                return "{\"active\" : true }";
            }
        }
    }
}