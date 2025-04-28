using Microsoft.EntityFrameworkCore;
using Project_2.Data;
using Project_2.Models;

namespace Project_2.Services.Services;

public interface IOfferService
{

    public Task<IEnumerable<Offer>> GetAllAsync();

    public Task<Offer?> GetByIdAsync(Guid guid);

    public Task AddAsync(Offer offer);
    public Task RemoveAsync(Offer offer);

    public Task<IEnumerable<Offer>> GetAllForProperty();

    public Task<IEnumerable<Offer>> GetAllByUser();
}