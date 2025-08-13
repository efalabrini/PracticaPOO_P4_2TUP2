namespace Core.Entities;

public class BankAccount
{
    //Nuevo: número de cuenta estático para generar números únicos
    private static int s_accountNumberSeed = 1234567890;

    public string Number { get; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public decimal Balance { get; }

    //Nuevo: Constructor
    public BankAccount(string name, decimal initialBalance)
    {
        Owner = name;
        Balance = initialBalance;

        //Nuevo: asignación de número de cuenta único 
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
    }

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
       
    }
}
