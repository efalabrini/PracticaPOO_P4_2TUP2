namespace Core.Entities
{
    public class BankAccount
    {
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance { get; private set; }

        public BankAccount(string number, string owner, decimal balance = 0)
        {
            Number = number;
            Owner = owner;
            Balance = balance;
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            Balance += amount;
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            Balance -= amount;
        }

        //Método para mostrar la información
        public string GetAccountInfo()
        {
            return $"El dueño de la cuenta {Owner} tiene {Balance:C} en su cuenta y su número de cuenta es {Number}.";
        }
    }
}