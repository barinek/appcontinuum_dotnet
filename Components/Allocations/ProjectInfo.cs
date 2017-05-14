using System.Runtime.Serialization;

namespace Allocations
{
    [DataContract(Name="info")]
    public class ProjectInfo
    {
        [DataMember(Name="active")]
        public bool Active { get; set; }

        public ProjectInfo(bool active)
        {
            Active = active;
        }
    }
}