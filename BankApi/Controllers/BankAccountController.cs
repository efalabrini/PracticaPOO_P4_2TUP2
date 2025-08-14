using Microsoft.AspNetCore.Mvc;
using Classes; // para acceder a BankAccount

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : ControllerBase
    {
        private static List<BankAccount> accounts = new List<BankAccount>();

        [HttpPost("create")]
        public IActionResult CreateAccount(string number, string owner)
        {
            var account = new BankAccount
            {
                Number = number,
                Owner = owner
            };

            accounts.Add(account);
            return Ok(account);
        }

        [HttpPost("deposit")]
        public IActionResult Deposit(string number, decimal amount, string note)
        {
            var account = accounts.FirstOrDefault(a => a.Number == number);
            if (account == null) return NotFound("Account not found");

            account.MakeDeposit(amount, DateTime.Now, note);
            return Ok(account);
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw(string number, decimal amount, string note)
        {
            var account = accounts.FirstOrDefault(a => a.Number == number);
            if (account == null) return NotFound("Account not found");

            account.MakeWithdrawal(amount, DateTime.Now, note);
            return Ok(account);
        }
    }
}
