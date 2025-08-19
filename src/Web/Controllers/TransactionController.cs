using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private static readonly Dictionary<string, BankAccount> Accounts = BankAccountController.Accounts;

        [HttpPost("deposit")]
        public ActionResult Deposit([FromQuery] string accountNumber, [FromQuery] decimal amount, [FromQuery] string? note = "")
        {
            if (!Accounts.TryGetValue(accountNumber, out var account))
                return NotFound("Cuenta no encontrada.");

            try
            {
                account.MakeDeposit(amount, DateTime.Now, note ?? "Depósito");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { message = "Depósito realizado con éxito", balance = account.Balance });
        }

        [HttpPost("withdraw")]
        public ActionResult Withdraw([FromQuery] string accountNumber, [FromQuery] decimal amount, [FromQuery] string? note = "")
        {

            //Busca un valor según su clave, en este caso el número de cuenta
            if (!Accounts.TryGetValue(accountNumber, out var account))
                return NotFound("Cuenta no encontrada.");
                
            try
            {
                account.MakeWithdrawal(amount, DateTime.Now, note ?? "Retiro");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { message = "Retiro realizado con éxito", balance = account.Balance });
        }

        [HttpGet("balance")]
        public ActionResult GetBalance([FromQuery] string accountNumber)
        {
            if (!Accounts.TryGetValue(accountNumber, out var account))
                return NotFound("Cuenta no encontrada.");

            return Ok(new { balance = account.Balance });
        }
    }
}
