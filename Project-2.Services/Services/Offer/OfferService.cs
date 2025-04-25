using Microsoft.EntityFrameworkCore;
using Project_2.Data;
using Project_2.Models;

namespace Project_2.Services.Services;

public class OfferService : IOfferService
{
    private readonly IOfferRepository _offerRepository;

    public OfferService(IOfferRepository offerRepository)
    {
        _offerRepository = offerRepository;
    }

    public async Task<IEnumerable<Offer>> GetAllAsync()
    {
        return await _offerRepository.GetAllAsync();
    }

    public async Task<Offer?> GetByIdAsync(Guid guid)
    {
        return await _offerRepository.GetByIdAsync(guid);
    }

    public async Task AddAsync(Offer offer)
    {
        await _offerRepository.AddAsync(offer);
        await _offerRepository.SaveChangesAsync();
    }

    public async Task RemoveAsync(Offer offer)
    {
        _offerRepository.Remove(offer);
        await _offerRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Offer>> GetAllForProperty()
    {
        return await _offerRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Offer>> GetAllByUser()
    {
        return await _offerRepository.GetAllAsync();
    }
}