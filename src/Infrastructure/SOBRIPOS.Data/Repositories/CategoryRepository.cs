using Microsoft.EntityFrameworkCore;
using SOBRIPOS.Application.Interfaces.Repositories;
using SOBRIPOS.Core.Entities;
using SOBRIPOS.Data.Contexts;

namespace SOBRIPOS.Data.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Name == name && !c.IsDeleted);
    }
}
