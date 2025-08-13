namespace Core.Entities;

public class BankAccount
{
    public string Number { get; }
    public string Owner { get; set; } 
    public decimal Balance { get; }

    public BankAccount(string owner, decimal initialBalance)
        {
            Owner = owner;
            Balance = initialBalance;
        }


    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
    }

    public void MakeWithDrawal(decimal amount, DateTime date, string note)
    {
    }
}