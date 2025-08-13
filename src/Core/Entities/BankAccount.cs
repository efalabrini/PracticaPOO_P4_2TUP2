namespace Core.Entities;

public class BankAccount
{
    public string Number { get; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public decimal Balance { get; }

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
    }
}