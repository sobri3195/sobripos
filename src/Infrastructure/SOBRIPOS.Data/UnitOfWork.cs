using SOBRIPOS.Application.Interfaces;
using SOBRIPOS.Application.Interfaces.Repositories;
using SOBRIPOS.Data.Contexts;
using SOBRIPOS.Data.Repositories;

namespace SOBRIPOS.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IProductRepository? _products;
    private ICategoryRepository? _categories;
    private ITransactionRepository? _transactions;
    private IUserRepository? _users;
    private ICustomerRepository? _customers;
    private ISupplierRepository? _suppliers;
    private IPurchaseRepository? _purchases;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IProductRepository Products => _products ??= new ProductRepository(_context);
    public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);
    public ITransactionRepository Transactions => _transactions ??= new TransactionRepository(_context);
    public IUserRepository Users => _users ??= new UserRepository(_context);
    public ICustomerRepository Customers => _customers ??= new CustomerRepository(_context);
    public ISupplierRepository Suppliers => _suppliers ??= new SupplierRepository(_context);
    public IPurchaseRepository Purchases => _purchases ??= new PurchaseRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
