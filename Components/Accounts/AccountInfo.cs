namespace Accounts
{
    public class AccountInfo
    {
        public long Id { get; }
        public long OwnerId { get; }
        public string Name { get; }
        public string Info { get; }

        public AccountInfo(long id, long ownerId, string name, string info)
        {
            Id = id;
            OwnerId = ownerId;
            Name = name;
            Info = info;
        }
    }
}