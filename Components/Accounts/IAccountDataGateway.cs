namespace Accounts
{
    public interface IAccountDataGateway
    {
        AccountRecord Create(long ownerId, string name);

        AccountRecord FindBy(long ownerId);
    }
}