using Microsoft.EntityFrameworkCore;
using SOBRIPOS.Application.Interfaces.Repositories;
using SOBRIPOS.Core.Entities;
using SOBRIPOS.Data.Contexts;

namespace SOBRIPOS.Data.Repositories;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    public SupplierRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Supplier?> GetByNameAsync(string name)
    {
        return await _dbSet
            .FirstOrDefaultAsync(s => s.Name == name && !s.IsDeleted);
    }
}
