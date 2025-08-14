using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateBankAccountController : ControllerBase
{
    [HttpPost]
    public ActionResult<string> createAccount([FromQuery] string name)
    public IActionResult CreateAccount([FromQuery] string name)
    {
        BankAccount cuenta = new(name, 0);
        return Ok(cuenta);
    }
}