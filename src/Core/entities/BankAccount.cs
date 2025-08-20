namespace Core.Entities;

public class BankAccount
{
    private static int s_accountNumberSeed = 1234567890;
    public string Number { get; }
    public string Owner { get; set; }

    public readonly string Type;
    private readonly decimal _minimumBalance;

    public record WithdrawalResult(Transaction Withdrawal, Transaction? OverLimitFee);
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }

            return balance;
        }
    }


    public BankAccount(string name, decimal initialBalance, string type) : this(name, initialBalance, type ,0) { }
    public BankAccount(string name, decimal initialBalance, string type ,decimal minimumBalance)
    {
        Owner = name;
        Type = type;
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
        _minimumBalance = minimumBalance;
        if (initialBalance > 0)
        {
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

    }

    private List<Transaction> _allTransactions = new List<Transaction>();

    public Transaction MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }
        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
        return deposit;
    }

    public WithdrawalResult MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
        }
        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);
        _allTransactions.Add(withdrawal);
        if (overdraftTransaction != null)
        {
            _allTransactions.Add(overdraftTransaction);
            return new(withdrawal, overdraftTransaction);
        }
        return new(withdrawal, null);
    }

    public virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException("Not sufficient funds for this withdrawal");
        }
        else
        {
            return default;
        }
    }

    public string GetAccountHistory()
    {
        var report = new System.Text.StringBuilder();

        decimal balance = 0;
        report.AppendLine("Date\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
        }

        return report.ToString();
    }
    
    
    public virtual void PerformMonthEndTransactions() { }

}



