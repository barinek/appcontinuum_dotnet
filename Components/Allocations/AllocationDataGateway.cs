using System;
using System.Collections.Generic;
using DatabaseSupport;

namespace Allocations
{
    public class AllocationDataGateway : IAllocationDataGateway
    {
        private readonly IDatabaseTemplate _template;

        public AllocationDataGateway(IDatabaseTemplate template)
        {
            _template = template;
        }

        public AllocationRecord Create(long projectId, long userId, DateTime firstDay, DateTime lastDay)
        {
            var sql = @"
insert into allocations (project_id, user_id, first_day, last_day) values (@projectId, @userId, @firstDay, @lastDay)";

            return _template.Create(sql, id => new AllocationRecord(id, projectId, userId, firstDay, lastDay),
                new KeyValuePair<string, object>("@projectId", projectId),
                new KeyValuePair<string, object>("@userId", userId),
                new KeyValuePair<string, object>("@firstDay", firstDay),
                new KeyValuePair<string, object>("@lastDay", lastDay)
            );
        }

        public List<AllocationRecord> FindBy(long projectId)
        {
            const string sql = @"
select id, project_id, user_id, first_day, last_day from allocations where project_id = @projectId order by first_day";

            return _template.FindBy(sql, reader => new AllocationRecord(
                reader.GetInt64(0),
                reader.GetInt64(1),
                reader.GetInt64(2),
                reader.GetDateTime(3),
                reader.GetDateTime(4)
            ), "@projectId", projectId);
        }
    }
}