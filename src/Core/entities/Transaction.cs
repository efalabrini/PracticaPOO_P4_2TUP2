namespace Core.Entities;

public class Transaction
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Notes { get; set; }

    public Transaction(decimal amount, DateTime date, string notes)
    {
        Amount = amount;
        Date = date;
        Notes = notes;
    }
}