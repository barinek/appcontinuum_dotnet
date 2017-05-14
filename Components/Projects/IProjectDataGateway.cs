using System.Collections.Generic;

namespace Projects
{
    public interface IProjectDataGateway
    {
        ProjectRecord Create(long accountId, string name);

        List<ProjectRecord> FindBy(long accountId);

        ProjectRecord FindObject(long projectId);
    }
}