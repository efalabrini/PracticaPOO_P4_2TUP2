using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BankAccountController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateAccount([FromQuery] string name, [FromQuery] int balance)
    {
        BankAccount cuenta = new(name, balance);
        return Ok(cuenta);
    }
}