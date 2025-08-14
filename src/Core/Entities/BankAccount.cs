namespace Core.Entities
{
    public class BankAccount
    {
        // Lista de transacciones
        private List<Transaction> _allTransactions = new();

        // Campo estático: se comparte entre TODAS las cuentas
        private static int s_accountNumberSeed = 1234567890;
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in _allTransactions)
                {
                    balance += item.Amount;
                }
                
                return balance;
            }
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "El monto ingresado debe ser positivo.");
            }
            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "El monto ingresado debe ser positivo.");
            }

            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Saldo insuficiente.");
            }

            var withdrawal = new Transaction(-amount, date, note);
            _allTransactions.Add(withdrawal);
        }

        // Constructor:
        public BankAccount(string name, decimal initialBalance)
        {
            this.Owner = name;
            this.Number = s_accountNumberSeed.ToString();
            s_accountNumberSeed++;

            MakeDeposit(initialBalance, DateTime.Now, "Balance Inicial");
        }        
    }
}