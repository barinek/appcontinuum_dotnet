using System.Threading.Tasks;

namespace Timesheets
{
    public interface IProjectClient
    {
        Task<ProjectInfo> Get(long projectId);
    }
}