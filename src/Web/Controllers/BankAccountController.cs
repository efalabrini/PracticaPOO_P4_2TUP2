using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Models.Requests;
using Core.Services;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BankAccountController : ControllerBase
{
   
    private readonly BankAccountService _bankAccountService;

    public BankAccountController(BankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;

    }

    
    [HttpPost("create")]
    public ActionResult<BankAccountDto> CreateBankAccount([FromBody] CreateBankAccountRequest bankAccountDto)
    {
        var newAccount = _bankAccountService.CreateBankAccount(bankAccountDto.OwnerId
         , bankAccountDto.InitialBalance
         , bankAccountDto.AccountType
         , bankAccountDto.CreditLimit
         , bankAccountDto.MonthlyDeposit);

        return CreatedAtAction(nameof(GetAccountInfo), new { accountNumber = newAccount.Number }, BankAccountDto.Create(newAccount));
    }

    [HttpPost("monthEnd")]
    public IActionResult PerformMonthEndForAccount([FromQuery] string accountNumber)
    {
        _bankAccountService.PerformMonthEndForAccount(accountNumber);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("deposit")]
    public IActionResult MakeDeposit([FromBody] MakeDepositRequest depositDto)
    {

        _bankAccountService.MakeDeposit(
                   depositDto.Amount,
                   depositDto.Notes,
                   depositDto.Number
               );
        return NoContent();
    }
    
    [HttpPost("withdrawal")]
    
     public IActionResult MakeWithdrawal([FromBody] MakeWithdrawalRequest withdrawalDto)
    {
            _bankAccountService.MakeWithdrawal(
                        withdrawalDto.Amount,
                        withdrawalDto.Notes,
                        withdrawalDto.Number
                    );
            return NoContent();
    }

    [HttpGet("balance")]
    public ActionResult<decimal> GetBalance([FromQuery] string accountNumber)
    {
        var balance = _bankAccountService.GetBalance(accountNumber);
        return balance;

    }

    [HttpGet("accountHistory")]
      public ActionResult<List<TransactionDto>> GetAccountHistory([FromQuery] string accountNumber)
    {
        var history = _bankAccountService.GetAccountHistory(accountNumber);
        return history;
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