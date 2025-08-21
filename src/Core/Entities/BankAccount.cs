using System;
using System.Text;
using System.Collections.Generic;

namespace Core.Entities;


public class BankAccount
{
    private static int s_accountNumberSeed = 1234567890; // S_ convencion para campos estaticos

    public string Number { get; private set; }
    public string Owner { get; set; }


    private List<Transaction> _allTransactions = new List<Transaction>(); // por convencion va con_ porque es privado

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
                throw new ArgumentOutOfRangeException(nameof(initialBalance), "El saldo inicial no puede ser negativo");

            Number = accountNumberSeed.ToString();
            s_accountNumberSeed++;

            Owner = name;
            MakeDeposit(initialBalance, DateTime.Now, "saldo inicial");
        }

    public void MakeDeposit(decimal amount, DateTime date, string note)
   {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "El monto del deposito debe ser positivo");

            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);
        }

   public void MakeWithdrawal(decimal amount, DateTime date, string note)
     {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "El monto del retiro debe ser positivo");

            if (Balance - amount < 0)
                throw new InvalidOperationException("No se puede retirar mas del saldo disponible");

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
    /* public IEnumerable<Transaction> GetAccountHistory()
{
    return _allTransactions; // Devuelve la lista de transacciones tal cual
}   */
}