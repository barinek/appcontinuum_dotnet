using System;
using System.Collections.Generic;
using DatabaseSupport;
using Microsoft.AspNetCore.Mvc;
using TestSupport;
using Timesheets;
using Xunit;

namespace TimesheetsTest
{
    [Collection("Timesheets")]
    public class TimeEntryControllerTest
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
                new TimeEntryController(new TimeEntryDataGateway(new DatabaseTemplate(_dataSourceConfig)),
                    new ProjectClient("http://localhost:3001"));

            var value = controller.Post(new TimeEntryInfo(-1, 55432, 4765, DateTime.Parse("2015-05-17"), 8, ""));
            var actual = (TimeEntryInfo) ((ObjectResult) value).Value;

            Assert.True(actual.Id > 0);
            Assert.Equal(55432, actual.ProjectId);
            Assert.Equal(4765, actual.UserId);
            Assert.Equal(17, actual.Date.Day);
            Assert.Equal(8, actual.Hours);
            Assert.Equal("entry info", actual.Info);

            server.Stop();
        }

        [Fact]
        public void TestGet()
        {
            _testScenarioSupport.LoadTestScenario("jacks-test-scenario");

            var controller =
                new TimeEntryController(new TimeEntryDataGateway(new DatabaseTemplate(_dataSourceConfig)),
                    new ProjectClient());
            var result = controller.Get(4765);

            // todo...
            Assert.Equal(2, ((List<TimeEntryInfo>) ((ObjectResult) result).Value).Count);
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