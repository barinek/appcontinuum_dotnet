using System;

namespace Allocations
{
    public class AllocationInfo
    {
        public long Id { get; }
        public long ProjectId { get; }
        public long UserId { get; }
        public DateTime FirstDay { get; }
        public DateTime LastDay { get; }
        public string Info { get; }


        public AllocationInfo(long id, long projectId, long userId, DateTime firstDay, DateTime lastDay, string info)
        {
            Id = id;
            ProjectId = projectId;
            UserId = userId;
            FirstDay = firstDay;
            LastDay = lastDay;
            Info = info;
        }
    }
}