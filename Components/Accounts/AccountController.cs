using Microsoft.AspNetCore.Mvc;

namespace Accounts
{
    [Route("accounts"), Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly IAccountDataGateway _gateway;

        public AccountController(IAccountDataGateway gateway)
        {
            _gateway = gateway;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int ownerId)
        {
            var record = _gateway.FindBy(ownerId);

            if (record != null)
            {
                return Ok(new AccountInfo(record.Id, record.OwnerId, record.Name, "account info"));
            }
            return NotFound();
        }
    }
}