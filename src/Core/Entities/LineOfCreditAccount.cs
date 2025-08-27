using PracticaPOO_P4_2TUP2.Core.Entities;

public class LineOfCreditAccount : BankAccount
{
   public LineOfCreditAccount(string name, decimal initialBalance) : base(name, initialBalance)
   {
   }
   public override void PerformMonthEndTransactions()
   {
      if (Balance < 0)
      {
         decimal interest = -Balance * 0.07m;

         MakeWithdrawal(interest, DateTime.Now, "Charge monthly interest");
      }

   }

}