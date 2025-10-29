using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Dtos;
namespace Core.Services;

public class BankAccountService
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IUserRepository _userRepository;

    public BankAccountService(IBankAccountRepository bankAccountRepository, IUserRepository userRepository)
    {
        _bankAccountRepository = bankAccountRepository;
        _userRepository = userRepository;
    }

    public BankAccount CreateBankAccount(int OwnerId,
    decimal InitialBalance,
    AccountType AccountType,
    decimal? CreditLimit = null,
    decimal? MonthlyDeposit = null)
    {
        var user = _userRepository.GetById(OwnerId);

        if (user is null)
            throw new AppValidationException("User not found.");

        BankAccount newAccount;

        switch (AccountType)
        {
            case AccountType.Credit:
                if (CreditLimit == null)
                    throw new AppValidationException("Credit limit is required for a Line of Credit account.");

                newAccount = new LineOfCreditAccount(user, InitialBalance, CreditLimit.Value);
                break;
            case AccountType.Gift:
                newAccount = new GiftCardAccount(user, InitialBalance, MonthlyDeposit ?? 0);
                break;
            case AccountType.Interest:
                newAccount = new InterestEarningAccount(user, InitialBalance);
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

    public decimal MakeDeposit(decimal amount, string note, string accountNumber)
    {
        var account = _bankAccountRepository.GetByAccountNumber(accountNumber)
            ?? throw new AppValidationException("Cuenta no encontrada.");

        account.MakeDeposit(amount, DateTime.Now, note);
        _bankAccountRepository.Update(account);

        return amount;
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



    public void MakeWithdrawal(decimal amount, string note, string accountNumber)
    {
        var account = _bankAccountRepository.GetByAccountNumber(accountNumber)
            ?? throw new AppValidationException("Cuenta no encontrada.");

        account.MakeWithdrawal(amount, DateTime.Now, note);
        _bankAccountRepository.Update(account);
    }

    public List<TransactionDto> GetAccountHistory(string accountNumber)
    {
        // Traemos la cuenta por número
        var account = _bankAccountRepository.GetByAccountNumber(accountNumber)
            ?? throw new AppValidationException("Cuenta no encontrada.");

        // Obtenemos sus transacciones usando la propiedad de navegación
        var transactions = account.Transactions
            .OrderByDescending(t => t.Date)
            .ToList();

        // Mapear a DTO
        return TransactionDto.Create(transactions);
    }
    public void PerformMonthEndForAccount(string accountNumber)
    {
        var account = _bankAccountRepository.GetByAccountNumber(accountNumber)
            ?? throw new AppValidationException("Cuenta no encontrada.");

        account.PerformMonthEndTransactions();
        _bankAccountRepository.Update(account);

    }
}
