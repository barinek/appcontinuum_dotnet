namespace Projects
{
    public class ProjectRecord
    {
        public long Id { get; }
        public long AccountId { get; }
        public string Name { get; }
        public bool Active { get; }


        public ProjectRecord(long id, long accountId, string name, bool active)
        {
            Id = id;
            AccountId = accountId;
            Name = name;
            Active = active;
        }
    }
}