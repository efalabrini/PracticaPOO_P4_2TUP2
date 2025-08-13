using Core.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {

        [HttpPost("BankAccountCreate")]
        public ActionResult<BankAccount> CreateBankAccount([FromQuery] string name, [FromQuery] decimal initialBalance)
        {
            var account = new BankAccount(name, initialBalance);
            return Ok(account);
        }
    }
}
