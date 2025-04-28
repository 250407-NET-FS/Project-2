using Project_2.Models;
using Project_2.Models.DTOs;

namespace Project_2.Services;

public interface IOfferService
{
    public Task<IEnumerable<OfferResponseDTO>> GetAllAsync();
    public Task<OfferResponseDTO?> GetByIdAsync(Guid id);
    public Task<OfferResponseDTO> AddAsync(OfferNewDTO dto);
    public Task RemoveAsync(Guid offerId);
    public Task<IEnumerable<OfferResponseDTO>> GetAllForProperty(Guid propertyId);
    public Task<IEnumerable<OfferResponseDTO>> GetAllByUser(Guid userId);
    public Task<IEnumerable<OfferResponseDTO>> SearchOffersAsync(OfferSearchDTO dto);
}