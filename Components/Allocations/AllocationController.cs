using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Allocations
{
    [Route("allocations"), Produces("application/json")]
    public class AllocationController : Controller
    {
        private readonly IAllocationDataGateway _gateway;
        private readonly IProjectClient _client;

        public AllocationController(IAllocationDataGateway gateway, IProjectClient client)
        {
            _gateway = gateway;
            _client = client;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int projectId)
        {
            var records = _gateway.FindBy(projectId);
            var list = records.Select(record => new AllocationInfo(record.Id, record.ProjectId, record.UserId,
                    record.FirstDay, record.LastDay, "allocation info"))
                .ToList();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AllocationInfo info)
        {
            if (!ProjectIsActive(info.ProjectId)) return new StatusCodeResult(304);

            var record = _gateway.Create(info.ProjectId, info.UserId, info.FirstDay, info.LastDay);
            var value = new AllocationInfo(record.Id, record.ProjectId, record.UserId, record.FirstDay, record.LastDay,
                "allocation info");
            return Created($"allocations/{value.Id}", value);
        }

        private bool ProjectIsActive(long projectId)
        {
            var info = _client.Get(projectId);
            return info.Result?.Active ?? false;
        }
    }
}