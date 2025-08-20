using Microsoft.AspNetCore.Mvc;
using Core.Entities;


namespace PracticaPOO_P4_2TUP2.Controllers


{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : ControllerBase
    {
        private static List<BankAccount> accounts = new List<BankAccount>();// como hacer para que este con una base de datos?

        [HttpPost("create")] // Endpoint para crear una cuenta
        public ActionResult<BankAccount> CreateAccount([FromQuery] string owner, [FromQuery] decimal initialBalance)
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
        [HttpGet("all")]
        public ActionResult<IEnumerable<BankAccount>> GetAccounts()
        {
            return Ok(accounts);
        }



        // Obtener cuenta por número
        [HttpGet("get/{number}")]
        public ActionResult<BankAccount> GetByNumber(string number)
        {
            var account = accounts.FirstOrDefault(a => a.Number == number);
            if (account == null)
                return NotFound($"No se encontró la cuenta con número {number}.");

            return Ok(account);
        }

        // Retirar dinero
        [HttpPost("withdraw")]
        public ActionResult<BankAccount> Withdraw([FromQuery] string accountNumber, [FromQuery] decimal amount)
        {
            var account = accounts.FirstOrDefault(a => a.Number == accountNumber);
            if (account == null)
                return NotFound($"No se encontró la cuenta con número {accountNumber}.");

            try
            {
                account.MakeWithdrawal(amount, DateTime.Now, "Retiro desde API");
                return Ok(account);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Depositar dinero
        [HttpPost("deposit")]
        public ActionResult<BankAccount> Deposit([FromQuery] string accountNumber, [FromQuery] decimal amount)
        {
            var account = accounts.FirstOrDefault(a => a.Number == accountNumber);
            if (account == null)
                return NotFound($"No se encontró la cuenta con número {accountNumber}.");

            try
            {
                account.MakeDeposit(amount, DateTime.Now, "Depósito desde API");
                return Ok(account);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("history/{number}")]
        public ActionResult<string> GetHistory(string number)
        {
            var account = accounts.FirstOrDefault(a => a.Number == number);
            if (account == null)
                return NotFound($"No se encontró la cuenta con número {number}.");

            return Ok(account.GetAccountHistory());
        }
    }


}