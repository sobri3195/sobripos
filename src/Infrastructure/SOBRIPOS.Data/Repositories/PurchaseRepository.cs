using Microsoft.EntityFrameworkCore;
using SOBRIPOS.Application.Interfaces.Repositories;
using SOBRIPOS.Core.Entities;
using SOBRIPOS.Data.Contexts;

namespace SOBRIPOS.Data.Repositories;

public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Purchase>> GetBySupplierAsync(Guid supplierId)
    {
        return await _dbSet
            .Include(p => p.Items)
            .ThenInclude(i => i.Product)
            .Include(p => p.Supplier)
            .Where(p => p.SupplierId == supplierId && !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<Purchase?> GetByPurchaseNumberAsync(string purchaseNumber)
    {
        return await _dbSet
            .Include(p => p.Items)
            .ThenInclude(i => i.Product)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.PurchaseNumber == purchaseNumber && !p.IsDeleted);
    }
}
