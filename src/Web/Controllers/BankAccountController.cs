using Microsoft.AspNetCore.Mvc;
using Core.Entities;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : Controller
    {
        [HttpPost("create")]
        public IActionResult CreateAccount([FromQuery] string name, [FromQuery] decimal balance = 10000)
        {
            var account = new BankAccount(name, balance);
            return Ok($"la cuenta se creo con el nombre: {account.Owner} y la cantidad de dinero es {balance}");
        }

    }
}
