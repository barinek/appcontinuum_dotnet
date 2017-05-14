using System.Runtime.Serialization;

namespace Backlog
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