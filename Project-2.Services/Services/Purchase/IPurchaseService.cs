using Project_2.Models;

namespace Project_2.Services.Services;

public interface IPurchaseService
{
    Task<IEnumerable<Purchase>> GetAllAsync();
    Task<Purchase?> GetByIdAsync(Guid id);
    Task AcceptOffer(Guid purchaseId, Guid offerId);
}