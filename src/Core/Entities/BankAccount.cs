namespace Classes;

public class BankAccount
{
  private static int s_accountNumberSeed = 1234567890;
  public string Number { get; }
  public string Owner { get; set; }
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

  private static readonly List<BankAccount> accounts = [];

  public static void AddAccount(BankAccount account)
  {
    accounts.Add(account);
  }

  public static List<BankAccount> GetAllAccounts()
  {
    return accounts;
  }

  public static BankAccount? GetAccountByNumber(string number)
  {
    BankAccount? account = accounts.FirstOrDefault(a => a.Number == number);

    return account;
  }

  public void MakeDeposit(decimal amount, DateTime date, string note)
  {
    if (amount < 0)
    {
      throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
    }
    var deposit = new Transaction(amount, date, note);
    _allTransactions.Add(deposit);
  }

  public void MakeWithdrawal(decimal amount, DateTime date, string note)
  {
    if (amount <= 0)
    {
      throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
    }
    if (Balance - amount < 0)
    {
      throw new InvalidOperationException("Not sufficient funds for this withdrawal");
    }
    var withdrawal = new Transaction(-amount, date, note);
    _allTransactions.Add(withdrawal);
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

  public BankAccount(string name, decimal initialBalance)
  {
    Number = s_accountNumberSeed.ToString();
    s_accountNumberSeed++;

    Owner = name;
    MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
  }

  private List<Transaction> _allTransactions = [];

}