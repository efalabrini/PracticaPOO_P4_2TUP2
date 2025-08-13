using Microsoft.AspNetCore.Mvc;
using Core.Entities;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]

public class BankAccountController : ControllerBase
{
    [HttpPost]
    public IActionResult POSTnewAccount([FromQuery] string name , [FromQuery] decimal balance)
    {
        try
        {
            var newAccount = new BankAccount(name, balance);
            if (newAccount == null)
            {
                return BadRequest("Error al crear la cuenta");
            }
            return Ok($"Nueva cuenta creada:\nNumer: {newAccount.Number}\nDueño: {newAccount.Owner}\nBalance inicial: {newAccount.Balance}");
        }
        catch (Exception )
        {
            return StatusCode(500, "error interno del server");
        }
    }
}
