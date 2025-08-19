namespace Core.Entities
{
    public class BankAccount
    {
        private static int s_accountNumberSeed = 1234567890;

        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance { get;  }

        // Constructor
        public BankAccount(string owner, decimal initialBalance)
        {
            Owner = owner;
            Balance = initialBalance;
            Number = s_accountNumberSeed.ToString();
            s_accountNumberSeed++;
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
           
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            
        }
    }
}