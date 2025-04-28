using Project_2.Models;

namespace Project_2.Services;

public interface IOfferService
{
    Task<IEnumerable<Offer>> GetAllAsync();
    Task<Offer?> GetByIdAsync(Guid guid);
    Task AddAsync(Offer offer);
    Task<IEnumerable<Offer>> GetAllForProperty(Guid propertyId);
}