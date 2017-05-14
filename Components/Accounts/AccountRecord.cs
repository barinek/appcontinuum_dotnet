namespace Accounts
{
    public class AccountRecord
    {
        public long Id { get; }
        public long OwnerId { get; }
        public string Name { get; }

        public AccountRecord(long id, long ownerId, string name)
        {
            Id = id;
            OwnerId = ownerId;
            Name = name;
        }
    }
}