using Microsoft.AspNetCore.Mvc;
using Core.Entities;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BankAccountController : ControllerBase
{
    private static List<BankAccount> accounts = new List<BankAccount>();

    [HttpPost("create")]
    public ActionResult<string> CreateBankAccount([FromQuery] string name, [FromQuery] decimal initialBalance)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("El nombre del propietario es obligatorio.");

            var newAccount = new BankAccount(name, initialBalance);

            accounts.Add(newAccount);

            return Ok($"Account {newAccount.Number} was created for {newAccount.Owner} with {newAccount.Balance} initial balance.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpPost("deposit")]
    public ActionResult<string> MakeDeposit([FromQuery] decimal amount, [FromQuery] string note, [FromQuery] string accountNumber)
    {
        try
        {
            var account = accounts.FirstOrDefault(a => a.Number == accountNumber);

            if (account == null)
                return NotFound("Cuenta no encontrada.");

            account.MakeDeposit(amount, DateTime.Now, note);

            return Ok($"A deposit of ${amount} was made in account {account.Number}.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpPost("withdrawal")]
    public ActionResult<string> MakeWithdrawal([FromQuery] decimal amount, [FromQuery] string note, [FromQuery] string accountNumber)
    {
        try
        {
            var account = accounts.FirstOrDefault(a => a.Number == accountNumber);

            if (account == null)
                return NotFound("Cuenta no encontrada.");

            account.MakeWithdrawal(amount, DateTime.Now, note);

            return Ok($"A withdrawal of ${amount} was made in account {account.Number}.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpGet("balance")]
    public ActionResult<string> GetBalance([FromQuery] string accountNumber)
    {
        try
        {
            var account = accounts.FirstOrDefault(a => a.Number == accountNumber);

            if (account == null)
                return NotFound("Cuenta no encontrada.");

            return Ok($"The balance in account {account.Number} is ${account.Balance}.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
    [HttpGet("accountHistory")]
    public IActionResult GetAccountHistory([FromQuery] string accountNumber)
    {
        try
        {
            var account = accounts.FirstOrDefault(a => a.Number == accountNumber);

            if (account == null)
                return NotFound("Cuenta no encontrada.");

            var history = account.GetAccountHistory();

            return Ok(history);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

}