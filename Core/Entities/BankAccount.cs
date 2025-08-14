using System;

namespace Classes;

public class BankAccount
{
    public string Number { get; }
    public string Owner { get; set; }
    public decimal Balance { get; }

    public void MakeDeposit(decimal amount, DateTime date, string note)
{
    if (amount <= 0)
    {
        throw new ArgumentOutOfRangeException(nameof(amount), "la cantidad del depósito debe ser positiva");
    }
    Balance += amount;
}

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
{
    if (amount <= 0)
    {
        throw new ArgumentOutOfRangeException(nameof(amount), "la cantidad del retiro debe ser positiva");
    }
    if (amount > Balance)
    {
        throw new InvalidOperationException("Nno hay suficiente saldo para realizar el retiro");
    }
    Balance -= amount;
}

}