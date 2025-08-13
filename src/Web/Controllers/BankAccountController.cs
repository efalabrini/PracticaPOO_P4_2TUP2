using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankAccountController : ControllerBase
{
    [HttpPost]
    public ActionResult<object> CreateAccount([FromQuery] string name)
    {
        var cuenta = new BankAccount("0001", name, 0m);

        return Ok(new
        {
            Name = cuenta.Owner,
            Number = cuenta.Number,
            Balance = cuenta.Balance
        });
    }
}
