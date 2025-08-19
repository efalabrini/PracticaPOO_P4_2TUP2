using Microsoft.AspNetCore.Mvc;
using Core.Entities;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]

public class BankAccountController : ControllerBase
{
    private static readonly List<BankAccount> accounts = new List<BankAccount>();

    [HttpPost]
    public IActionResult POSTnewAccount([FromQuery] string name, [FromQuery] decimal balance, [FromQuery] string accountType, [FromQuery] decimal creditLimit, [FromQuery] decimal monthlyDeposit)
    {
        try
        {
            var newAccount = accountType switch
            {
                "normal" => new BankAccount(name, balance, "Normal account"),

                "interest-earning" => new InterestEarningAccount(name, balance, "Interest Earning Account"),

                "line-credit" => new LineOfCreditAccount(name, balance, "Line Of Credit Account", creditLimit),

                "gift-card" => new GiftCardAccount(name, balance, "Gift Card Account", monthlyDeposit),

                _ => throw new Exception("account type not available")
            };
            if (newAccount == null)
            {
                return BadRequest("Error al crear la cuenta");
            }
            accounts.Add(newAccount);
            return Ok($"Nueva cuenta creada:\nNumer: {newAccount.Number}\nDueño: {newAccount.Owner}\nBalance inicial: {newAccount.Balance}\nTipo: {newAccount.Type}");
        }
        catch (Exception)
        {
            return StatusCode(500, "error interno del server");
        }
    }

    [HttpGet]
    public IActionResult GETallAccounts()
    {
        try
        {
            return Ok(accounts);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("deposit/{aNumber}")]
    public IActionResult POSTdeposit([FromBody] Transaction depositData, [FromRoute] string aNumber)
    {
        try
        {
            var account = accounts.FirstOrDefault(a => a.Number == aNumber);
            if (account == null)
            {
                return NotFound($"Account with id: {aNumber} not found");
            }
            var deposit = account.MakeDeposit(depositData.Amount, depositData.Date, depositData.Notes);
            if (deposit == null)
            {
                throw new Exception("Internal server error");
            }
            return Ok(new { deposit = deposit, balance = account.Balance });
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("withdrawal/{aNumber}")]
    public IActionResult POSTwithdrawal([FromBody] Transaction withdrawalData, [FromRoute] string aNumber)
    {
        try
        {
            var account = accounts.FirstOrDefault(a => a.Number == aNumber);
            if (account == null)
            {
                return NotFound($"Account with id: {aNumber} not found");
            }
            var withdrawal = account.MakeWithdrawal(withdrawalData.Amount, withdrawalData.Date, withdrawalData.Notes);
            if (withdrawal == null)
            {
                throw new Exception("Internal server error");
            }
            return Ok(new { withdrawal = withdrawal, balance = account.Balance });
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("balance/{aNumber}")]
    public IActionResult GETbalanceById([FromRoute] string aNumber)
    {
        try
        {
            var account = accounts.FirstOrDefault(a => a.Number == aNumber);
            if (account == null)
            {
                return NotFound($"Account with id: {aNumber} not found");
            }
            return Ok(account.Balance);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("accountHistory/{aNumber}")]
    public IActionResult GETAccountHistory([FromRoute] string aNumber)
    {
        try
        {
            var account = accounts.FirstOrDefault(a => a.Number == aNumber);
            if (account == null)
            {
                return NotFound($"Account with {aNumber} ID not found");
            }
            return Ok(account.GetAccountHistory());
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }



}


