using System.Collections.Generic;

namespace Backlog
{
    public interface IStoryDataGateway
    {
        StoryRecord Create(long projectId, string name);

        List<StoryRecord> FindBy(long projectId);
    }
}