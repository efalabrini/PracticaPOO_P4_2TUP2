using Microsoft.AspNetCore.Mvc;
using PracticaPOO_P4_2TUP2.Core.Entities;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    [HttpPost("/MakeDeposit")]
    public IActionResult MakeDeposit([FromQuery] decimal balance, [FromQuery] string owner, [FromQuery] string? note = "Transacción completada")
    {
        try
        {
            // Buscar la cuenta por owner usando la lista estática de BankAccountController
            var account = BankAccountController.accounts.FirstOrDefault(a => a.Owner == owner);
            if (account == null)
            {
                return NotFound($"No se encontró la cuenta para el propietario: {owner}");
            }
            // Realizar el depósito
            account.MakeDeposit(balance, DateTime.Now, note ?? "Transacción completada");
            return Ok(account);
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor {ex.Message}");
        }

    }

    [HttpPost("/MakeWithdrawal")]
    public IActionResult MakeWithdrawal([FromQuery] decimal balance, [FromQuery] string owner, string? note = "Retiro completado")
    {
        try
        {
            var account = BankAccountController.accounts.FirstOrDefault(a => a.Owner == owner);
            if (account == null)
            {
                return NotFound($"No se encontró la cuenta para el propietario: {owner}");
            }

            account.MakeWithdrawal(balance, DateTime.Now, note ?? "Retiro completado");
            return Ok(account);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}