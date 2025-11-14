using Microsoft.EntityFrameworkCore;
using SOBRIPOS.Application.Interfaces.Repositories;
using SOBRIPOS.Core.Entities;
using SOBRIPOS.Data.Contexts;

namespace SOBRIPOS.Data.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Product?> GetByBarcodeAsync(string barcode)
    {
        return await _dbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Barcode == barcode && !p.IsDeleted);
    }

    public async Task<Product?> GetBySKUAsync(string sku)
    {
        return await _dbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.SKU == sku && !p.IsDeleted);
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.StockQuantity <= p.MinimumStock && !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
            .ToListAsync();
    }
}
