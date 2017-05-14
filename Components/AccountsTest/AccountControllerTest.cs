using Accounts;
using DatabaseSupport;
using Microsoft.AspNetCore.Mvc;
using TestSupport;
using Xunit;

namespace AccountsTest
{
    [Collection("Accounts")]
    public class AccountControllerTest
    {
        private readonly TestDataSourceConfig _dataSourceConfig = new TestDataSourceConfig();
        private readonly TestScenarioSupport _testScenarioSupport = new TestScenarioSupport();

        [Fact]
        public void TestGet()
        {
            _testScenarioSupport.LoadTestScenario("jacks-test-scenario");

            var controller =
                new AccountController(new AccountDataGateway(new DatabaseTemplate(_dataSourceConfig)));
            var result = controller.Get(4765);

            var info = (AccountInfo) ((ObjectResult) result).Value;

            Assert.Equal(1673, info.Id);
            Assert.Equal(4765, info.OwnerId);
            Assert.Equal("Jack's account", info.Name);
            Assert.Equal("account info", info.Info);
        }
    }
}