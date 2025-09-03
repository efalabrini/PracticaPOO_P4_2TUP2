namespace Core.Entities
{
    public class GiftCardAccount : BankAccount
    {
        public override string  Type => "GiftCardAccount";
        private readonly decimal _monthlyDeposit = 0m;

        public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0) 
            : base(name, initialBalance)
        {
            _monthlyDeposit = monthlyDeposit;
        }

        public override void PerformMonthEndTransactions()
        {
            if (_monthlyDeposit != 0)
            {
                MakeDeposit(_monthlyDeposit, DateTime.Now, "Add monthly deposit");
            }
        }
    }
}