using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Backlog
{
    [Route("stories"), Produces("application/json")]
    public class StoryController : Controller
    {
        private readonly IStoryDataGateway _gateway;
        private readonly IProjectClient _client;

        public StoryController(IStoryDataGateway gateway, IProjectClient client)
        {
            _gateway = gateway;
            _client = client;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int projectId)
        {
            var records = _gateway.FindBy(projectId);
            var list = records.Select(record => new StoryInfo(record.Id, record.ProjectId, record.Name, "story info"))
                .ToList();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Post([FromBody] StoryInfo info)
        {
            if (!ProjectIsActive(info.ProjectId)) return new StatusCodeResult(304);

            var record = _gateway.Create(info.ProjectId, info.Name);
            var value = new StoryInfo(record.Id, record.ProjectId, record.Name, "story info");
            return Created($"stories/{value.Id}", value);
        }

        private bool ProjectIsActive(long projectId)
        {
            var info = _client.Get(projectId);
            return info.Result?.Active ?? false;
        }
    }
}