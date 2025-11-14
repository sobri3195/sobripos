using SOBRIPOS.Core.Entities;

namespace SOBRIPOS.Application.Interfaces.Repositories;

public interface IPurchaseRepository : IRepository<Purchase>
{
    Task<IEnumerable<Purchase>> GetBySupplierAsync(Guid supplierId);
    Task<Purchase?> GetByPurchaseNumberAsync(string purchaseNumber);
}
