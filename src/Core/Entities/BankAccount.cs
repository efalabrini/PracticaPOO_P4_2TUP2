using System.Data;
namespace Core.Entities;
public class BankAccount
{
    public string Owner { get; }
    public decimal Balance { get; private set; }
    public string Number { get; }
    public BankAccount(string owner, decimal initialBalance)
    {
        Owner = owner;
        Balance = initialBalance;
        Number = Guid.NewGuid().ToString();
    }
}