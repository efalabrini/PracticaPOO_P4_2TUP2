using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateBankAccountController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateAccount([FromQuery] string name)
    {
        BankAccount cuenta = new(name, 0);
        return Ok(cuenta);
    }
}