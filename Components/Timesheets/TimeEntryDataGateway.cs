using System;
using System.Collections.Generic;
using DatabaseSupport;

namespace Timesheets
{
    public class TimeEntryDataGateway : ITimeEntryDataGateway
    {
        private readonly IDatabaseTemplate _template;

        public TimeEntryDataGateway(IDatabaseTemplate template)
        {
            _template = template;
        }

        public TimeEntryRecord Create(long projectId, long userId, DateTime date, int hours)
        {
            var sql = @"
insert into time_entries (project_id, user_id, date, hours) values (@projectId, @userId, @date, @hours)";

            return _template.Create(sql, id => new TimeEntryRecord(id, projectId, userId, date, hours),
                new KeyValuePair<string, object>("@projectId", projectId),
                new KeyValuePair<string, object>("@userId", userId),
                new KeyValuePair<string, object>("@date", date),
                new KeyValuePair<string, object>("@hours", hours)
            );
        }

        public List<TimeEntryRecord> FindBy(long userId)
        {
            const string sql = @"
select id, project_id, user_id, date, hours from time_entries where user_id = @userId";

            return _template.FindBy(sql, reader => new TimeEntryRecord(
                reader.GetInt64(0),
                reader.GetInt64(1),
                reader.GetInt64(2),
                reader.GetDateTime(3),
                reader.GetInt32(4)
            ), "@userId", userId);
        }
    }
}