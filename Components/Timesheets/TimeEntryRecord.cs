using System;

namespace Timesheets
{
    public class TimeEntryRecord
    {
        public long Id { get; }
        public long ProjectId { get; }
        public long UserId { get; }
        public DateTime Date { get; }
        public int Hours { get; }


        public TimeEntryRecord(long id, long projectId, long userId, DateTime date, int hours)
        {
            Id = id;
            ProjectId = projectId;
            UserId = userId;
            Date = date;
            Hours = hours;
        }
    }
}