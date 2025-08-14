using Classes;

namespace PracticaPOO_P4_2TUP2.Core.Entities;

public class BankAccount
{
    public string Number { get; set; }
    public string Owner { get; set; }
    public decimal Balance { get; private set; }

    private static int s_accountNumberSeed = 1234567890;


    private List<Transaction> _allTransactions = [];


    public BankAccount(string owner, decimal initialBalance = 0)
    {
        Owner = owner;
        Balance = initialBalance;
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
    }

}