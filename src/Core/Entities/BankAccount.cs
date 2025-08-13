namespace Core.Entities;

public class BankAccount
{
    public string Number { get; }
    public string Owner { get; set; }
    public decimal Balance { get; private set; }

    // Constructor
    public BankAccount(string number, string owner, decimal initialBalance)
    {
        Number = number;
        Owner = owner;
        Balance = initialBalance;
    }

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        Balance += amount;
    }

    public void MakeWithDrawal(decimal amount, DateTime date, string note)
    {
        Balance -= amount;
    }
}
