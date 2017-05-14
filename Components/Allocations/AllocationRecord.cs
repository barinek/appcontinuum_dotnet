using System;

namespace Allocations
{
    public class AllocationRecord
    {
        public long Id { get; }
        public long ProjectId { get; }
        public long UserId { get; }
        public DateTime FirstDay { get; }
        public DateTime LastDay { get; }


        public AllocationRecord(long id, long projectId, long userId, DateTime firstDay, DateTime lastDay)
        {
            Id = id;
            ProjectId = projectId;
            UserId = userId;
            FirstDay = firstDay;
            LastDay = lastDay;
        }
    }
}