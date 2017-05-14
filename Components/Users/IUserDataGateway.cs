namespace Users
{
    public interface IUserDataGateway
    {
        UserRecord Create(string name);

        UserRecord FindObjectBy(long id);
    }
}