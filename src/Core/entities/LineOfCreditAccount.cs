using System.Reflection.Metadata;

namespace Core.Entities;

public class LineOfCreditAccount : BankAccount
{
    public LineOfCreditAccount(string name, decimal initialBalance, string type ,decimal creditLimit) : base(name, initialBalance, type ,-creditLimit)
    {

    }
    public override void PerformMonthEndTransactions()
    {
        if (Balance < 0)
        {
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.Now, "Charge monthly interest");
        }
    }
}