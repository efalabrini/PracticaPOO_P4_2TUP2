using Classes;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BankAccountController : ControllerBase
{
  [HttpPost]
  public IActionResult CreateBankAccount([FromQuery] string name)
  {
    BankAccount account = new(name, 0);
    BankAccount.AddAccount(account);
    return Ok(account);
  }

  [HttpGet]
  public IActionResult GetAllAccounts()
  {
    return Ok(BankAccount.GetAllAccounts());
  }
}