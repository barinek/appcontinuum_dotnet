using System.Threading.Tasks;

namespace Allocations
{
    public interface IProjectClient
    {
        Task<ProjectInfo> Get(long projectId);
    }
}