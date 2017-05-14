using System;

namespace Timesheets
{
    public class TimeEntryInfo
    {
        public long Id { get; }
        public long ProjectId { get; }
        public long UserId { get; }
        public DateTime Date { get; }
        public int Hours { get; }
        public string Info { get; }


        public TimeEntryInfo(long id, long projectId, long userId, DateTime date, int hours, string info)
        {
            Id = id;
            ProjectId = projectId;
            UserId = userId;
            Date = date;
            Hours = hours;
            Info = info;
        }
    }
}