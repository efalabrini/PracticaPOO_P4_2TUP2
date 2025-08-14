using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateAccount([FromBody] BankAccount account)
        {
            if (string.IsNullOrWhiteSpace(account.Owner))
            {
                return BadRequest("El titular es obligatorio");
            }
            return Ok($"Cuenta creada para {account.Owner} con saldo inicial de {account.Balance}");
        }
    }
    public class BankAccount
    {
        public string Owner { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}