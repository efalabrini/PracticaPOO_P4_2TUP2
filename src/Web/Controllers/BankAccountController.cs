using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {
        //Diccionario para guardar en memoriaa todas las cuentas => Key(Número de cuenta) Value(Las instancias que vamos creando)
        public static readonly Dictionary<string, BankAccount> Accounts = new(); 

        [HttpPost]
        public ActionResult<BankAccount> CreateAccount([FromQuery] string? owner, [FromQuery] decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(owner))
            {
                return BadRequest("El propietario de la cuenta es obligatorio.");
            }

            if (initialBalance <= 0)
            {
                return BadRequest("El saldo inicial debe ser mayor a 0.");
            }

            var account = new BankAccount(owner, initialBalance);
            //Accedemos a la key (número de cuenta)
            Accounts[account.Number] = account;

            return Ok(account); 
        }
    }
}