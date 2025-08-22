using Classes;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
  [HttpPost("/MakeDeposit")]
  public IActionResult MakeDeposit([FromQuery] decimal balance, [FromQuery] string number, [FromQuery] string note = "Transacción completada")
  {
    // Buscar la cuenta por owner con el método estático
    var account = BankAccount.GetAccountByNumber(number);
    if (account == null)
    {
      return NotFound($"No se encontró la cuenta para el propietario: {number}");
    }
    // Realizar el depósito
    account.MakeDeposit(balance, DateTime.Now, note);
    return Ok(account);
  }

  [HttpGet("/MakeWithdrawal")]
  public IActionResult MakeWithdrawal([FromQuery] decimal balance, [FromQuery] string number, [FromQuery] string note = "Retiro completado")
  {
    // Buscar la cuenta por owner con el método estático
    var account = BankAccount.GetAccountByNumber(number);
    if (account == null)
    {
      return NotFound($"No se encontró la cuenta para el propietario: {number}");
    }

    account.MakeWithdrawal(balance, DateTime.Now, note);
    return Ok(account);
  }

  [HttpGet("/TransactionHistory")]
  public IActionResult TransactionHistory([FromQuery] string number)
  {
    var account = BankAccount.GetAccountByNumber(number);
    if (account == null)
    {
      return NotFound($"No se encontró la cuenta para el propietario: {number}");
    }
    return Ok(account.GetAccountHistory());
  }

}