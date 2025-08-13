using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateBankAccount : ControllerBase
{
    [HttpPost]
    public ActionResult<string> createAccount([FromQuery] string name)
    {
        BankAccount cuenta = new(name, 0);
        return $"Cuenta de {name} creada. \n Número de cuenta: {cuenta.Number} \n Balance: {cuenta.Balance} AR$";
    }
}