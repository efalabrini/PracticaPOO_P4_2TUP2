namespace Core.Entities;

public class InterestEarningAccount : BankAccount
{
    public InterestEarningAccount(string name, decimal initialBalance, string type) : base(name, initialBalance, type)
    {
    }

    public override void PerformMonthEndTransactions()
    {
        if (Balance > 500m)
        {
            decimal intereses = Balance * 0.02m;
            MakeDeposit(intereses, DateTime.Now, "apply monthly interest");
        }
    }
}