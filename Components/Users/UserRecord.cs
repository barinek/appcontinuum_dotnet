namespace Users
{
    public class UserRecord
    {
        public long Id { get; }
        public string Name { get; }

        public UserRecord(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}