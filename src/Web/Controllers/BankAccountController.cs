using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BankAccountController : ControllerBase
{
    // Es una caja de todas las cuentas que existen
    private static Dictionary<string, BankAccount> _accounts = new();

    [HttpPost("create")]
    public IActionResult CreateAccount([FromQuery] string name, [FromQuery] int balance)
    {
        BankAccount cuenta = new(name, balance);
        _accounts.Add(cuenta.Number, cuenta);

        return Ok(new
        {
            Message = "Cuenta creada exitosamente.",
            AccountNumber = cuenta.Number,
            Owner = cuenta.Owner,
            Balance = cuenta.Balance
        });
    }

    [HttpPost("deposit")]
    public IActionResult MakeDeposit([FromQuery] string accountNumber, [FromQuery] decimal amount, [FromQuery] string note)
    {
        if (!_accounts.ContainsKey(accountNumber))
        {
            return NotFound("Cuenta no encontrada.");
        }

        var cuenta = _accounts[accountNumber];

        try
        {
            cuenta.MakeDeposit(amount, DateTime.Now, note);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(new
        {
            Message = $"Depósito de {amount} AR$ realizado con éxito.",
            accountNumber = cuenta.Number,
            Transaction = cuenta
        });
    }

    [HttpPost("withdraw")]
    public IActionResult MakeWithdrawal([FromQuery] string accountNumber, [FromQuery] decimal amount, [FromQuery] string note)
    {
        if (!_accounts.ContainsKey(accountNumber))
        {
            return NotFound("Cuenta no encontrada.");
        }

        var cuenta = _accounts[accountNumber];

        try
        {
            cuenta.MakeWithdrawal(amount, DateTime.Now, note);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest($"Error de argumento: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest($"Error de operación: {ex.Message}");
        }

        return Ok(new
        {
            Message = $"Retiro de {amount} AR$ realizado con éxito.",
            accountNumber = cuenta.Number,
            Owner = cuenta.Owner,
            Balance = cuenta.Balance
        });
    }
}