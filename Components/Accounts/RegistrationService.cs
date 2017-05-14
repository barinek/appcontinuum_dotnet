using Users;

namespace Accounts
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserDataGateway _userDataGateway;
        private readonly IAccountDataGateway _accountDataGateway;

        public RegistrationService(IUserDataGateway userDataGateway, IAccountDataGateway accountDataGateway)
        {
            _userDataGateway = userDataGateway;
            _accountDataGateway = accountDataGateway;
        }

        public UserRecord CreateUserWithAccount(string name)
        {
            // todo - make transactional
            var user = _userDataGateway.Create(name);
            _accountDataGateway.Create(user.Id, $"{name}'s account");
            return user;
        }
    }
}