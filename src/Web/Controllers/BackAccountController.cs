using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {
        [HttpPost("create")]
        public ActionResult<string> CreateAccount([FromQuery] string name, [FromQuery] decimal balance = 0)
        {
            BankAccount cuenta = new(name, balance);
            return $"Cuenta a nombre de : {name}, Nº {cuenta.Number}, Balance: {cuenta.Balance} ";
        }
    }
}



 