namespace Users
{
    public class UserInfo
    {
        public long Id { get; }
        public string Name { get; }
        public string Info { get; }

        public UserInfo(long id, string name, string info)
        {
            Id = id;
            Name = name;
            Info = info;
        }
    }
}