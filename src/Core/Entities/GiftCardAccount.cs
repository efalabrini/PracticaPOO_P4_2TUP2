namespace PracticaPOO_P4_2TUP2.Core.Entities;


public class GiftCardAccount : BankAccount
{
  private decimal _monthlyDeposit;

  public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit) : base(name, initialBalance)
  {
    _monthlyDeposit = monthlyDeposit;
  }

  public override void PerformMonthEndTransactions()
  {
    if (_monthlyDeposit > 0)
    {
      MakeDeposit(_monthlyDeposit, DateTime.Now, "Add monthly deposit");
    }
  }
}
