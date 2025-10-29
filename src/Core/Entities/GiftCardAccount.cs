namespace Core.Entities
{
    public class GiftCardAccount : BankAccount
    {
        private readonly decimal _monthlyDeposit = 0m;

        public GiftCardAccount(User owner, decimal initialBalance, decimal monthlyDeposit = 0) 
            : base(owner, initialBalance)
        {
            _monthlyDeposit = monthlyDeposit;
        }

        private GiftCardAccount() // private: asegura el encapsulamiento
        {
            
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