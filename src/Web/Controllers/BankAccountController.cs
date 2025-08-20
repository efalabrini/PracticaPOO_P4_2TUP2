using Microsoft.AspNetCore.Mvc;
using Core.Entities;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateAccount([FromQuery] string owner, [FromQuery] decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(owner))
            {
                return BadRequest("El titular es obligatorio");
            }
            var account = new BankAccount(owner, initialBalance);
            return Ok(new
            {
                Message = $"Cuenta creada para {account.Owner} con saldo {account.Balance}",
                account.Number,
                account.Owner,
                account.Balance
            });
        }
    }
}