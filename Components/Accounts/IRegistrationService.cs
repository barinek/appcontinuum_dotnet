using Users;

namespace Accounts
{
    public interface IRegistrationService
    {
        UserRecord CreateUserWithAccount(string name);
    }
}