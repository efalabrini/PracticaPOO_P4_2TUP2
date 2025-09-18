using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public BankAccountRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public BankAccount Add(BankAccount entity)
    {
        _applicationDbContext.bankAccounts.Add(entity);
        _applicationDbContext.SaveChanges();
        return entity;
    }

    public void Delete(BankAccount entity)
    {
        _applicationDbContext.bankAccounts.Remove(entity);
        _applicationDbContext.SaveChanges();
    }

    public BankAccount GetByAccountNumber(string accountNumber)
    {
        return _applicationDbContext.bankAccounts.FirstOrDefault(a => a.Number == accountNumber);
    }

    public BankAccount GetById(int id)
    {
        return _applicationDbContext.bankAccounts.FirstOrDefault(a => a.Id == id);
    }

    public List<BankAccount> List()
    {
        return _applicationDbContext.bankAccounts.ToList();
    }

    public BankAccount Update(BankAccount entity)
    {
        _applicationDbContext.bankAccounts.Update(entity);
        _applicationDbContext.SaveChanges();
        return entity;
    }
}