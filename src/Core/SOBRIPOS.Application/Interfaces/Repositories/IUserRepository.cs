using SOBRIPOS.Core.Entities;
using SOBRIPOS.Core.Enums;

namespace SOBRIPOS.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetByRoleAsync(UserRole role);
}
