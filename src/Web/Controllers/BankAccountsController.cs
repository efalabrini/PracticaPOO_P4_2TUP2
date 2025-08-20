using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PracticaPOO_P4_2TUP2.Core.Entities;

[ApiController]
[Route("[controller]")]

// primer punto de partida, todo arranca en el codigo del controller
public class BankAccountController : ControllerBase
{
  public static readonly List<BankAccount> accounts = [];

  [HttpPost]
  public IActionResult CreateBankAccount([FromQuery] string owner, [FromQuery] decimal initialBalance)
  {
    try
    {
      BankAccount account = new(owner, initialBalance);
      accounts.Add(account);
      return Ok(account);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Error interno del servidor: {ex.Message}");
    }

  }

  [HttpGet("getOneAccount/{id}")]
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


  [HttpGet]
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

}


