using System.Globalization;

namespace Core.Entities;

public class InterestEarningAccount : BankAccount
{
    public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance)
    {

    }
    public virtual void PerformMonthEndTransactions()
    {
        

    }

}