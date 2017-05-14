using Accounts;
using DatabaseSupport;
using Microsoft.AspNetCore.Mvc;
using TestSupport;
using Users;
using Xunit;

namespace AccountsTest
{
    [Collection("Accounts")]
    public class RegistrationControllerTest
    {
        private readonly TestDataSourceConfig _dataSourceConfig = new TestDataSourceConfig();
        private readonly TestScenarioSupport _testScenarioSupport = new TestScenarioSupport();

        [Fact]
        public void TestPost()
        {
            _testScenarioSupport.LoadTestScenario("jacks-test-scenario");

            var userDataGateway = new UserDataGateway(new DatabaseTemplate(_dataSourceConfig));
            var accountDataGateway = new AccountDataGateway(new DatabaseTemplate(_dataSourceConfig));
            var service = new RegistrationService(userDataGateway, accountDataGateway);

            var controller = new RegisationController(service);
            var value = controller.Post(new UserInfo(-1, "aUser", ""));
            var actual = (UserInfo) ((ObjectResult) value).Value;

            Assert.True(actual.Id > 0);
            Assert.Equal("aUser", actual.Name);
            Assert.Equal("registration info", actual.Info);
        }
    }
}