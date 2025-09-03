namespace Core.Entities
{
    public class InterestEarningAccount : BankAccount
    {
        public InterestEarningAccount(string name, decimal initialBalance, AccountType accountType)
            : base(name, initialBalance, accountType)
        {
        }

        public override void PerformMonthEndTransactions()
        {
            if (Balance > 500m)
            {
                decimal interest = Balance * 0.02m;
                MakeDeposit(interest, DateTime.Now, "apply monthly interest");
            }
        }
    }
}