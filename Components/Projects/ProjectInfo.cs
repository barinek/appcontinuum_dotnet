namespace Projects
{
    public class ProjectInfo
    {
        public long Id { get; }
        public long AccountId { get; }
        public string Name { get; }
        public bool Active { get; }
        public string Info { get; }


        public ProjectInfo(long id, long accountId, string name, bool active, string info)
        {
            Id = id;
            AccountId = accountId;
            Name = name;
            Active = active;
            Info = info;
        }
    }
}