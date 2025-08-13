using Microsoft.AspNetCore.Mvc;
using Core.Entities;

namespace WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {
        private static List<BankAccount> accounts = new List<BankAccount>();

        [HttpPost]
        public IActionResult Create([FromBody] BankAccount account)
        {
            accounts.Add(account);
            return Ok(account);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(accounts);
        }
    }
}