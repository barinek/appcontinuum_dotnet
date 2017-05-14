using System.Threading.Tasks;

namespace Backlog
{
    public interface IProjectClient
    {
        Task<ProjectInfo> Get(long projectId);
    }
}