using System.Collections.Generic;
using System.Linq;
using DatabaseSupport;

namespace Projects
{
    public class ProjectDataGateway : IProjectDataGateway
    {
        private readonly IDatabaseTemplate _template;

        public ProjectDataGateway(IDatabaseTemplate template)
        {
            _template = template;
        }

        public ProjectRecord Create(long accountId, string name)
        {
            var sql = @"
insert into projects (account_id, name, active) values (@accountId, @name, true)";

            return _template.Create(sql, id => new ProjectRecord(id, accountId, name, true),
                new KeyValuePair<string, object>("@accountId", accountId),
                new KeyValuePair<string, object>("@name", name)
            );
        }

        public List<ProjectRecord> FindBy(long accountId)
        {
            const string sql = @"
select id, account_id, name, active from projects where account_id = @accountId order by name asc";

            return _template.FindBy(sql, reader => new ProjectRecord(
                reader.GetInt64(0),
                reader.GetInt64(1),
                reader.GetString(2),
                reader.GetBoolean(3)
            ), "@accountId", accountId);
        }


        public ProjectRecord FindObject(long projectId)
        {
            const string sql = @"
select id, account_id, name, active from projects where id = @projectId order by name asc";

            var projectRecords = _template.FindBy(sql, reader => new ProjectRecord(
                reader.GetInt64(0),
                reader.GetInt64(1),
                reader.GetString(2),
                reader.GetBoolean(3)
            ), "@projectId", projectId);

            return projectRecords.Count > 0 ? projectRecords.First() : null;
        }
    }
}