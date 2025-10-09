using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;

namespace Core.Services;

public class BankAccountService
{
    private readonly IBankAccountRepository _bankAccountRepository;

    public BankAccountService(IBankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    public BankAccount CreateBankAccount(string Name,
    decimal InitialBalance,
    AccountType AccountType,
    decimal? CreditLimit = null,
    decimal? MonthlyDeposit = null)
    {
        BankAccount newAccount;

        switch (AccountType)
        {
            case AccountType.Credit:
                if (CreditLimit == null)
                    throw new AppValidationException("Credit limit is required for a Line of Credit account.");

                newAccount = new LineOfCreditAccount(Name, InitialBalance, CreditLimit.Value);
                break;
            case AccountType.Gift:
                newAccount = new GiftCardAccount(Name, InitialBalance, MonthlyDeposit ?? 0);
                break;
            case AccountType.Interest:
                newAccount = new InterestEarningAccount(Name, InitialBalance);
                break;
            default:
                throw new AppValidationException("Invalid account type.");
        }

        _bankAccountRepository.Add(newAccount);

        return newAccount;
    }

    public decimal GetBalance(string accountNumber)
    {
        var account = _bankAccountRepository.GetByAccountNumber(accountNumber)
        ?? throw new AppValidationException("Cuenta no encontrada.");

        return account.Balance;
    }

    public void MakeDeposit(decimal amount, string note, string accountNumber)
    {
        var account = _bankAccountRepository.GetByAccountNumber(accountNumber)
            ?? throw new AppValidationException("Cuenta no encontrada.");

        account.MakeDeposit(amount, DateTime.Now, note);
        _bankAccountRepository.Update(account);
    }

    public BankAccount GetAccountInfo(string accountNumber)
    {
        var account = _bankAccountRepository.GetByAccountNumber(accountNumber)
                ?? throw new AppValidationException("Cuenta no encontrada.");
        return account;
    }

    public List<BankAccount> GetAllAccountsInfo()
    {
        return _bankAccountRepository.ListWithTransaction();
    }

    public void PerformMonthEndForAccount(string accountNumber)
    {
        var account = _bankAccountRepository.GetByAccountNumber(accountNumber)
            ?? throw new AppValidationException("Cuenta no encontrada.");

        account.PerformMonthEndTransactions();
    }

    public void MakeWithdrawal(decimal amount, string note, string accountNumber)
    {
        var account = _bankAccountRepository.GetByAccountNumber(accountNumber)
            ?? throw new AppValidationException("Cuenta no encontrada.");

        account.MakeWithdrawal(amount, DateTime.Now, note);
        _bankAccountRepository.Update(account);
    }

    public string GetAccountHistory(string accountNumber)
    {
        var account = _bankAccountRepository.GetByAccountNumber(accountNumber)
            ?? throw new AppValidationException("Cuenta no encontrada.");

        return account.GetAccountHistory();
    }

}