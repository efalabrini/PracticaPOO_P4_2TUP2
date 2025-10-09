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
        var result = _bankAccountService.PerformMonthEndForAccount(accountNumber);
        return Ok(result);
    }

    [HttpPost("deposit")]
    public ActionResult<string> MakeDeposit([FromBody] MakeDepositRequest depositDto)
    {

        var depositedAmount = _bankAccountService.MakeDeposit(
            depositDto.Amount,
            depositDto.Note,
            depositDto.Number
        );
        return Ok(depositedAmount);
    }

    [HttpPost("withdrawal")]
    public ActionResult<string> MakeWithdrawal([FromBody] MakeWithdrawalRequest withdrawDto)
    {
        var withdrawalAmount = _bankAccountService.MakeDeposit(
            withdrawDto.Amount,
            withdrawDto.Note,
            withdrawDto.Number
        );

        return Ok(withdrawalAmount);
    }

    [HttpGet("balance")]
    public ActionResult<string> GetBalance([FromQuery] string accountNumber)
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