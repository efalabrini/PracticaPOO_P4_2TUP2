namespace Core.Entities;

public class BankAccount
{
    private static int accountNumberSeed = 1234567890;

    public string Number { get; private set; }
    public string Owner { get; set; }
    public decimal Balance { get; private set; }

    public BankAccount(string owner, decimal initialBalance)
    {
        this.Owner = owner;
        this.Balance = initialBalance;
        this.Number = accountNumberSeed.ToString();
        accountNumberSeed++;
    }

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        Balance += amount;
    }

   public void MakeWithdrawal(decimal amount, DateTime date, string note)
{
    if (amount <= 0)
        throw new ArgumentOutOfRangeException(nameof(amount), "El monto a retirar debe ser mayor que cero.");

    if (amount > Balance)
        throw new InvalidOperationException("Saldo insuficiente para realizar el retiro.");

    Balance -= amount;    //ojo! hay que agregar el endponint en el controller
}

}
