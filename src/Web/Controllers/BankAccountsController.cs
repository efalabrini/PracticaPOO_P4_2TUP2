using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PracticaPOO_P4_2TUP2.Core.Entities;

[ApiController]
[Route("[controller]")]

// primer punto de partida, todo arranca en el codigo del controller
public class BankAccountController : ControllerBase
{
  public static readonly List<BankAccount> accounts = [];

  [HttpPost("create")]
  public IActionResult CreateAccount([FromQuery] string name, [FromQuery] decimal initialBalance, [FromQuery] string accountType, [FromQuery] decimal? monthlyDeposit = null)
  {
    try
    {
      BankAccount account = accountType.ToLower() switch
      {
        "normal" or "bankaccount" => new BankAccount(name, initialBalance),
        "giftcard" => new GiftCardAccount(name, initialBalance, monthlyDeposit ?? 0),
        "interest" => new InterestEarningAccount(name, initialBalance),
        "lineofcredit" => new LineOfCreditAccount(name, initialBalance),
        _ => throw new ArgumentException($"Tipo de cuenta no válido: {accountType}. Tipos válidos: normal, giftcard, interest, lineofcredit")
      };
      
      accounts.Add(account);
      return Ok(new { 
        message = $"Cuenta {accountType} creada exitosamente",
        account = account 
      });
    }
    catch (ArgumentException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Error interno del servidor: {ex.Message}");
    }
  }
  // Token: User
  [HttpGet("User/{id}")]
  public IActionResult GetOneAccount([FromRoute] string id)
  {
    try
    {
      var account = accounts.FirstOrDefault(a => a.Number == id);
      if (account == null)
      {
        return NotFound($"No se encontró una cuenta con el número {id}");
      }
      return Ok(account);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Error interno de servidor: {ex.Message}");
    }

  }

  [HttpGet("Users")]
  public IActionResult GetAllAccounts()
  {
    try
    {
      return Ok(accounts);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Error interno del servidor: {ex.Message}");
    }

  }

  [HttpGet("history")]
  public IActionResult GetHistory([FromQuery] string owner)
  {
    var account = accounts.FirstOrDefault(a => a.Owner == owner);
    if (account == null)
    {
      return NotFound($"No se pudo encontrar un usuario llamado {owner}");
    }
    var history = account.GetAccountHistory();
    return Ok(history);
  }

  [HttpPost("month-end")]
  public IActionResult ProcessMonthEnd()
  {
    try
    {
      var results = new List<object>();
      
      foreach (var account in accounts)
      {
        var balanceBefore = account.Balance;
        account.PerformMonthEndTransactions();
        var balanceAfter = account.Balance;
        
        results.Add(new {
          AccountNumber = account.Number,
          Owner = account.Owner,
          AccountType = account.GetType().Name,
          BalanceBefore = balanceBefore,
          BalanceAfter = balanceAfter,
          Change = balanceAfter - balanceBefore
        });
      }
      
      return Ok(new { 
        message = "Procesamiento de fin de mes completado",
        totalAccounts = accounts.Count,
        results = results
      });
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Error al procesar fin de mes: {ex.Message}");
    }
  }



}
