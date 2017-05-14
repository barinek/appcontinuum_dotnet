using Microsoft.AspNetCore.Mvc;

namespace Users
{
    [Route("users"), Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IUserDataGateway _gateway;

        public UserController(IUserDataGateway gateway)
        {
            _gateway = gateway;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int userId)
        {
            var record = _gateway.FindObjectBy(userId);

            if (record != null)
            {
                return Ok(new UserInfo(record.Id, record.Name, "user info"));
            }
            return NotFound();
        }
    }
}