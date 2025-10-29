namespace Core.Entities
{
    public class LineOfCreditAccount : BankAccount
    {
        public LineOfCreditAccount(User owner, decimal initialBalance, decimal creditLimit)
            : base(owner, initialBalance, -creditLimit)  
        {
        }

        private LineOfCreditAccount()
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
        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) =>
            isOverdrawn
            ? new Transaction(-20, DateTime.Now, "Apply overdraft fee")
            : default;
    }
}
