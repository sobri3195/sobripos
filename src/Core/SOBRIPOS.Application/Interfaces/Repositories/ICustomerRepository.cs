using SOBRIPOS.Core.Entities;

namespace SOBRIPOS.Application.Interfaces.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);
    Task<Customer?> GetByEmailAsync(string email);
}
