using SOBRIPOS.Core.Entities;

namespace SOBRIPOS.Application.Interfaces.Repositories;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Transaction>> GetByCashierAsync(Guid cashierId);
    Task<IEnumerable<Transaction>> GetByCustomerAsync(Guid customerId);
    Task<Transaction?> GetByTransactionNumberAsync(string transactionNumber);
}
