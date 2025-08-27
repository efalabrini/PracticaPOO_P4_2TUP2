namespace PracticaPOO_P4_2TUP2.Core.Entities;

public class BankAccount
{
  private static int s_accountNumberSeed = 1234567890;
  public string Number { get; }
  public string Owner { get; set; }
  public decimal Balance
  {
    // PROPIEDAD BALANCE QUE HACE UN CALCULO AL OBTENERLO
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

  private List<Transaction> _allTransactions = [];

  // CONSTRUCTOR
  public BankAccount(string name, decimal initialBalance)
  {
    try
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentException("Owner name cannot be null or empty", nameof(name));
      }
      if (initialBalance < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance cannot be negative");
      }

      Number = s_accountNumberSeed.ToString();
      s_accountNumberSeed++;

      Owner = name;
      MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
    }
    catch (ArgumentOutOfRangeException ex)
    {
      throw new ArgumentOutOfRangeException($"[400 - Bad Request] {ex.Message}", ex.ParamName);
    }
    catch (ArgumentException ex)
    {
      throw new ArgumentException($"[400 - Bad Request] {ex.Message}", ex.ParamName);
    }
    catch (Exception ex)
    {
      throw new Exception($"[500 - Internal Server Error] Error creating bank account: {ex.Message}");
    }
  }

  // METODOS

  public void MakeDeposit(decimal amount, DateTime date, string note)
  {
    try
    {
      if (amount <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
      }
      if (string.IsNullOrWhiteSpace(note))
      {
        throw new ArgumentException("Note cannot be null or empty", nameof(note));
      }

      var deposit = new Transaction(amount, date, note);
      _allTransactions.Add(deposit);
    }
    catch (ArgumentOutOfRangeException ex)
    {
      throw new ArgumentOutOfRangeException($"[400 - Bad Request] {ex.Message}", ex.ParamName);
    }
    catch (ArgumentException ex)
    {
      throw new ArgumentException($"[400 - Bad Request] {ex.Message}", ex.ParamName);
    }
    catch (Exception ex)
    {
      throw new Exception($"[500 - Internal Server Error] Error making deposit: {ex.Message}");
    }
  }

  public void MakeWithdrawal(decimal amount, DateTime date, string note)
  {
    try
    {
      if (amount <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
      }
      if (string.IsNullOrWhiteSpace(note))
      {
        throw new ArgumentException("Note cannot be null or empty", nameof(note));
      }
      if (Balance - amount < 0)
      {
        throw new InvalidOperationException("Not sufficient funds for this withdrawal");
      }

      var withdrawal = new Transaction(-amount, date, note);
      _allTransactions.Add(withdrawal);
    }
    catch (ArgumentOutOfRangeException ex)
    {
      throw new ArgumentOutOfRangeException($"[400 - Bad Request] {ex.Message}", ex.ParamName);
    }
    catch (ArgumentException ex)
    {
      throw new ArgumentException($"[400 - Bad Request] {ex.Message}", ex.ParamName);
    }
    catch (InvalidOperationException ex)
    {
      throw new InvalidOperationException($"[409 - Conflict] {ex.Message}");
    }
    catch (Exception ex)
    {
      throw new Exception($"[500 - Internal Server Error] Error making withdrawal: {ex.Message}");
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


