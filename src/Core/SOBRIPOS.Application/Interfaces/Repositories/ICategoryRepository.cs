using SOBRIPOS.Core.Entities;

namespace SOBRIPOS.Application.Interfaces.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
}
