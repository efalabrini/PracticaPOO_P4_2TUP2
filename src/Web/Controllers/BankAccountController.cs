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

        [HttpGet("test-account")]
        public IActionResult TestAccount()
        {
            var account = new BankAccount("Alice", 1000);
            account.MakeWithdrawal(500, DateTime.Now, "Rent payment");

            account.MakeDeposit(100, DateTime.Now, "Friend paid me back");

            return Ok(new
            {
                account.Number,
                account.Owner,
                account.Balance
            });
        }
    }
}
