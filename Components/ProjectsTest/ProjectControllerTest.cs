using System.Collections.Generic;
using DatabaseSupport;
using Microsoft.AspNetCore.Mvc;
using Projects;
using TestSupport;
using Xunit;

namespace ProjectsTest
{
    [Collection("Projects")]
    public class ProjectControllerTest
    {
        private readonly TestDataSourceConfig _dataSourceConfig = new TestDataSourceConfig();
        private readonly TestScenarioSupport _testScenarioSupport = new TestScenarioSupport();

        [Fact]
        public void TestPost()
        {
            _testScenarioSupport.LoadTestScenario("jacks-test-scenario");

            var controller =
                new ProjectController(new ProjectDataGateway(new DatabaseTemplate(_dataSourceConfig)));

            var value = controller.Post(new ProjectInfo(-1, 1673, "aProject", true, ""));
            var actual = (ProjectInfo) ((ObjectResult) value).Value;

            Assert.True(actual.Id > 0);
            Assert.Equal(1673, actual.AccountId);
            Assert.Equal("aProject", actual.Name);
            Assert.Equal(true, actual.Active);
            Assert.Equal("project info", actual.Info);
        }

        [Fact]
        public void TestGet()
        {
            _testScenarioSupport.LoadTestScenario("jacks-test-scenario");

            var controller =
                new ProjectController(new ProjectDataGateway(new DatabaseTemplate(_dataSourceConfig)));
            var result = controller.Get(55431);
            var actual = (ProjectInfo) ((ObjectResult) result).Value;

            Assert.Equal(55431, actual.Id);
            Assert.Equal(1673, actual.AccountId);
            Assert.Equal("Hovercraft", actual.Name);
            Assert.Equal(false, actual.Active);
        }

        [Fact]
        public void TestList()
        {
            _testScenarioSupport.LoadTestScenario("jacks-test-scenario");

            var controller =
                new ProjectController(new ProjectDataGateway(new DatabaseTemplate(_dataSourceConfig)));
            var result = controller.List(1673);

            // todo - full asserts?
            Assert.Equal(2, ((List<ProjectInfo>) ((ObjectResult) result).Value).Count);
        }
    }
}