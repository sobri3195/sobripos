using SOBRIPOS.Application.Interfaces.Repositories;

namespace SOBRIPOS.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    ITransactionRepository Transactions { get; }
    IUserRepository Users { get; }
    ICustomerRepository Customers { get; }
    ISupplierRepository Suppliers { get; }
    IPurchaseRepository Purchases { get; }
    Task<int> SaveChangesAsync();
}
