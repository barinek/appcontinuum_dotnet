using System;
using System.Collections.Generic;

namespace Timesheets
{
    public interface ITimeEntryDataGateway
    {
        TimeEntryRecord Create(long projectId, long userId, DateTime date, int hours);

        List<TimeEntryRecord> FindBy(long userId);
    }
}