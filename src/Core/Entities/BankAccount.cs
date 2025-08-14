namespace Classes;

public class BankAccount
{
  private static int s_accountNumberSeed = 1234567890;
  public string Number { get; }
  public string Owner { get; set; }
  public decimal Balance { get; }

  public void MakeDeposit(decimal amount, DateTime date, string note)
  {
  }

  public void MakeWithdrawal(decimal amount, DateTime date, string note)
  {
  }

  public BankAccount(string name, decimal initialBalance)
  {
    Owner = name;
    Balance = initialBalance;

    Number = s_accountNumberSeed.ToString();
    s_accountNumberSeed++;
  }

}