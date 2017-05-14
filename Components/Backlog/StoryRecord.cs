namespace Backlog
{
    public class StoryRecord
    {
        public long Id { get; }
        public long ProjectId { get; }
        public string Name { get; }


        public StoryRecord(long id, long projectId, string name)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
        }
    }
}