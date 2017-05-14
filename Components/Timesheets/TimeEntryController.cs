using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Timesheets
{
    [Route("time-entries"), Produces("application/json")]
    public class TimeEntryController : Controller
    {
        private readonly ITimeEntryDataGateway _gateway;
        private readonly IProjectClient _client;

        public TimeEntryController(ITimeEntryDataGateway gateway, IProjectClient client)
        {
            _gateway = gateway;
            _client = client;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int userId)
        {
            var records = _gateway.FindBy(userId);
            var list = records.Select(record => new TimeEntryInfo(record.Id, record.ProjectId, record.UserId,
                    record.Date, record.Hours, "entry info"))
                .ToList();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TimeEntryInfo info)
        {
            if (!ProjectIsActive(info.ProjectId)) return new StatusCodeResult(304);

            var record = _gateway.Create(info.ProjectId, info.UserId, info.Date, info.Hours);
            var value = new TimeEntryInfo(record.Id, record.ProjectId, record.UserId, record.Date, record.Hours,
                "entry info");
            return Created($"time-entries/{value.Id}", value);
        }

        private bool ProjectIsActive(long projectId)
        {
            var info = _client.Get(projectId);
            return info.Result?.Active ?? false;
        }
    }
}