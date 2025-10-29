namespace Core.Entities
{
    public class InterestEarningAccount : BankAccount
    {
        public InterestEarningAccount(User owner, decimal initialBalance)
            : base(owner, initialBalance)
        {
        }

        private InterestEarningAccount()
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