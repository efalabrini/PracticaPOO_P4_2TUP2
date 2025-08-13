using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateAccount(string Owner, decimal Balance)
        {
            if (string.IsNullOrWhiteSpace(Owner))
            {
                return BadRequest("El titular es obligatorio");
            }
            return Ok($"Cuenta creada para {Owner} con saldo inicial de {Balance}");
        }
    }
}