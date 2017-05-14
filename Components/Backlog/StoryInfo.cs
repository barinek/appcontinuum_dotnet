namespace Backlog
{
    public class StoryInfo
    {
        public long Id { get; }
        public long ProjectId { get; }
        public string Name { get; }
        public string Info { get; }


        public StoryInfo(long id, long projectId, string name, string info)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
            Info = info;
        }
    }
}