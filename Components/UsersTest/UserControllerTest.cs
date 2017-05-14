using DatabaseSupport;
using Microsoft.AspNetCore.Mvc;
using TestSupport;
using Users;
using Xunit;

namespace UsersTest
{
    [Collection("Users")]
    public class UserControllerTest
    {
        private readonly TestDataSourceConfig _dataSourceConfig = new TestDataSourceConfig();
        private readonly TestScenarioSupport _testScenarioSupport = new TestScenarioSupport();

        [Fact]
        public void TestGet()
        {
            _testScenarioSupport.LoadTestScenario("jacks-test-scenario");

            var controller =
                new UserController(new UserDataGateway(new DatabaseTemplate(_dataSourceConfig)));
            var result = controller.Get(4765);
            var info = ((UserInfo) ((ObjectResult) result).Value);

            Assert.Equal(4765, info.Id);
            Assert.Equal("Jack", info.Name);
            Assert.Equal("user info", info.Info);
        }
    }
}