using Project_2.Models;
using Project_2.Models.DTOs;

namespace Project_2.Services.Services;

public interface IPurchaseService
{
    Task<IEnumerable<Purchase>> GetAllAsync();
    Task<Purchase?> GetByIdAsync(Guid id);
    Task AcceptOffer(CreatePurchaseDTO purchaseDTO);
}