using Microsoft.EntityFrameworkCore;
using SOBRIPOS.Application.Interfaces.Repositories;
using SOBRIPOS.Core.Entities;
using SOBRIPOS.Data.Contexts;

namespace SOBRIPOS.Data.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Include(t => t.Items)
            .ThenInclude(i => i.Product)
            .Include(t => t.Cashier)
            .Include(t => t.Customer)
            .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate && !t.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByCashierAsync(Guid cashierId)
    {
        return await _dbSet
            .Include(t => t.Items)
            .ThenInclude(i => i.Product)
            .Include(t => t.Customer)
            .Where(t => t.CashierId == cashierId && !t.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByCustomerAsync(Guid customerId)
    {
        return await _dbSet
            .Include(t => t.Items)
            .ThenInclude(i => i.Product)
            .Include(t => t.Cashier)
            .Where(t => t.CustomerId == customerId && !t.IsDeleted)
            .ToListAsync();
    }

    public async Task<Transaction?> GetByTransactionNumberAsync(string transactionNumber)
    {
        return await _dbSet
            .Include(t => t.Items)
            .ThenInclude(i => i.Product)
            .Include(t => t.Cashier)
            .Include(t => t.Customer)
            .FirstOrDefaultAsync(t => t.TransactionNumber == transactionNumber && !t.IsDeleted);
    }
}
