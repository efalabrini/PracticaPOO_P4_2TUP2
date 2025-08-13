using Microsoft.AspNetCore.Mvc;
using PracticaPOO_P4_2TUP2.Core.Entities;


namespace PracticaPOO_P4_2TUP2.Controllers


{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : ControllerBase
    {
        private static List<BankAccount> accounts = new List<BankAccount>();

        [HttpPost("create")]
        public ActionResult<BankAccount> CreateAccount(string owner, decimal initialBalance)
        {
            try
            {
                var account = new BankAccount(owner, initialBalance);
                accounts.Add(account);
                return Ok(account);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}