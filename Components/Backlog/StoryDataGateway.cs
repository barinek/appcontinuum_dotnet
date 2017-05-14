using System.Collections.Generic;
using DatabaseSupport;

namespace Backlog
{
    public class StoryDataGateway : IStoryDataGateway
    {
        private readonly IDatabaseTemplate _template;

        public StoryDataGateway(IDatabaseTemplate template)
        {
            _template = template;
        }

        public StoryRecord Create(long projectId, string name)
        {
            var sql = @"
insert into stories (project_id, name) values (@projectId, @name)";

            return _template.Create(sql, id => new StoryRecord(id, projectId, name),
                new KeyValuePair<string, object>("@projectId", projectId),
                new KeyValuePair<string, object>("@name", name)
            );
        }

        public List<StoryRecord> FindBy(long projectId)
        {
            const string sql = @"
select id, project_id, name from stories where project_id = @projectId";

            return _template.FindBy(sql, reader => new StoryRecord(
                reader.GetInt64(0),
                reader.GetInt64(1),
                reader.GetString(2)
            ), "@projectId", projectId);
        }
    }
}