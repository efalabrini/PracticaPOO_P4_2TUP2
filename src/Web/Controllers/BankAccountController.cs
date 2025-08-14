
using Classes;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    [HttpPost("create")]
    public IActionResult CreateAccount([FromQuery] string name, [FromQuery] decimal balance = 1000)
    {
        var account = new BankAccount(name, balance);

        return Ok($"La cuenta se creo con el nombre: {account.Number} y la cantidad de dinero es: {account.Balance} ");
    }
}
