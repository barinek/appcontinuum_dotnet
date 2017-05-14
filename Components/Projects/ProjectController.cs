using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Projects
{
    public class ProjectController : Controller
    {
        private readonly IProjectDataGateway _gateway;

        public ProjectController(IProjectDataGateway gateway)
        {
            _gateway = gateway;
        }

        [HttpGet]
        [Route("projects"), Produces("application/json")]
        public IActionResult List([FromQuery] int accountId)
        {
            var records = _gateway.FindBy(accountId);
            var list = records.Select(record => new ProjectInfo(record.Id, record.AccountId, record.Name,
                    record.Active, "project info"))
                .ToList();
            return Ok(list);
        }

        [HttpGet]
        [Route("project"), Produces("application/json")]
        public IActionResult Get([FromQuery] int projectId)
        {
            var record = _gateway.FindObject(projectId);

            if (record != null)
            {
                return Ok(new ProjectInfo(record.Id, record.AccountId, record.Name, record.Active, "project info"));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("projects"), Produces("application/json")]
        public IActionResult Post([FromBody] ProjectInfo info)
        {
            var record = _gateway.Create(info.AccountId, info.Name);
            var value = new ProjectInfo(record.Id, record.AccountId, record.Name, record.Active, "project info");
            return Created($"projects/{value.Id}", value);
        }
    }
}