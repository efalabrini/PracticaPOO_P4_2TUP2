using Microsoft.AspNetCore.Mvc;
using Core.Entities;

namespace WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {
        private static readonly List<BankAccount> accounts = [];

        [HttpPost]
        public IActionResult Create([FromBody] BankAccount account)
        {
            accounts.Add(account);
            return Ok(account);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var info = accounts.Select(a => a.GetAccountInfo()).ToList();
            return Ok(info);
        }

    }
}