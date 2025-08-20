using System;
using System.Text;

namespace Core.Entities;


public class BankAccount
{
    private static int accountNumberSeed = 1234567890;

    public string Number { get; private set; }
    public string Owner { get; set; }


    private List<Transaction> _allTransactions = new List<Transaction>();

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

    public BankAccount(string name, decimal initialBalance)
   {
            if (initialBalance < 0)
                throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance must be positive");

            Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            Owner = name;
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

    public void MakeDeposit(decimal amount, DateTime date, string note)
   {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");

            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);
        }

   public void MakeWithdrawal(decimal amount, DateTime date, string note)
     {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");

            if (Balance - amount < 0)
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");

            var withdrawal = new Transaction(-amount, date, note);
            _allTransactions.Add(withdrawal);
        }
    
        public string GetAccountHistory()
        {
            var report = new StringBuilder();
            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }
            return report.ToString();
        }
}
