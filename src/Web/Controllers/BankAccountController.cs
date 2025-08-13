using Microsoft.AspNetCore.Mvc;
using Core.Entities; 

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BankAccountController : ControllerBase
{

    [HttpPost]
    public ActionResult<string> CreateBankAccount([FromQuery] string name, [FromQuery] decimal initialBalance)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("El nombre del propietario es obligatorio.");
            if (initialBalance < 0)
                return BadRequest("El saldo inicial no puede ser negativo.");

            var newAccount = new BankAccount(name, initialBalance);

            return Ok($"Account {newAccount.Number} was created for {newAccount.Owner} with {newAccount.Balance} initial balance.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}