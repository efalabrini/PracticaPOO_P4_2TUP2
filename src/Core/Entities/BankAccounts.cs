// Nombrar correctamente por directorio
namespace PracticaPOO_P4_2TUP2.Core.Entities;

public class BankAccount
{
    public string Number { get; set; }
    public string Owner { get; set; }
    public decimal Balance { get; private set; }

    public BankAccount(string number, decimal initialBalance = 0)
    {
        Number = number;
        Balance = initialBalance;
    }

}