using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using System.Linq.Expressions;
using Web.Models;
using Core.Exceptions;
using Web.Models.Requests;
using Core.Services;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BankAccountController : ControllerBase
{
    private readonly BankAccountService _bankAccountService;

    public BankAccountController(BankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    [HttpPost("create")]
    public IActionResult CreateBankAccount([FromBody] CreateBankAccountRequest bankAccountDto)
    {
        var newAccount = _bankAccountService.CreateBankAccount(bankAccountDto.Name
         , bankAccountDto.InitialBalance
         , bankAccountDto.AccountType
         , bankAccountDto.CreditLimit
         , bankAccountDto.MonthlyDeposit);

        return CreatedAtAction(nameof(GetAccountInfo), new { accountNumber = newAccount.Number }, BankAccountDto.Create(newAccount));
    }

    [HttpPost("monthEnd")]
    public ActionResult<string> PerformMonthEndForAccount([FromQuery] string accountNumber)
    {
        _bankAccountService.PerformMonthEndForAccount(accountNumber);
        return NoContent();
    }

    [HttpPost("deposit")]
    public ActionResult<string> MakeDeposit([FromBody] MakeDepositRequest depositDto)
    {
        _bankAccountService.MakeDeposit(
                    depositDto.Amount,
                    depositDto.Note,
                    depositDto.Number
                );
        return NoContent();
    }

    [HttpPost("withdrawal")]
    public ActionResult<string> MakeWithdrawal([FromBody] MakeDepositRequest depositDto)
    {
        _bankAccountService.MakeWithdrawal(
                    depositDto.Amount,
                    depositDto.Note,
                    depositDto.Number
                );
        return NoContent();
    }

    [HttpGet("balance")]
    public ActionResult<decimal> GetBalance([FromQuery] string accountNumber)
    {
        var balance = _bankAccountService.GetBalance(accountNumber);
        return Ok(balance);

    }

    [HttpGet("accountHistory")]
    public IActionResult GetAccountHistory([FromQuery] string accountNumber)
    {
        var history = _bankAccountService.GetAccountHistory(accountNumber);

        return Ok(history);
    }

    [HttpGet("accountInfo")]
    public ActionResult<BankAccountDto> GetAccountInfo([FromQuery] string accountNumber)
    {
        var account = _bankAccountService.GetAccountInfo(accountNumber);

        return BankAccountDto.Create(account);
    }

    [HttpGet("allAccountsInfo")]
    public ActionResult<List<BankAccountDto>> GetAllAccountInfo()
    {
        var list = _bankAccountService.GetAllAccountsInfo();
        return BankAccountDto.Create(list);
    }
}