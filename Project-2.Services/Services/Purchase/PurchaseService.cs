using Project_2.Data;
using Project_2.Models;
using Project_2.Models.DTOs;

namespace Project_2.Services.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PurchaseService(IPurchaseRepository purchaseRepository, IUnitOfWork unitOfWork)
    {
        _purchaseRepository = purchaseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Purchase>> GetAllAsync()
    {
        return await _purchaseRepository.GetAllAsync();
    }

    public async Task<Purchase?> GetByIdAsync(Guid id)
    {
        return await _purchaseRepository.GetByIdAsync(id);
    }

    public async Task AcceptOffer(CreatePurchaseDTO purchaseDTO)
    {
        Property? property = (await _unitOfWork.PropertyRepo.GetByIdAsync(purchaseDTO.PropertyId))!;
        if (property is null || property.ForSale == false) {
            throw new Exception("Property not for sale");
        }

        if (property.OwnerID != purchaseDTO.UserId) {
            throw new Exception("Unauthorized");
        }

        Offer? offer = (await _unitOfWork.OfferRepo.GetByIdAsync(purchaseDTO.OfferId))!;
        if (offer is null) {
            throw new Exception("Offer does not exist");
        }

        // Insert record of new sale
        Purchase newPurchase = new Purchase(offer.UserID, property.PropertyID, offer.BidAmount); // use default datetime.now for time of purchase
        await _unitOfWork.PurchaseRepo.AddAsync(newPurchase);

        // Purge previous offers for the newly sold property
        _unitOfWork.OfferRepo.RemoveAllForProperty(property.PropertyID);

        // Update property to reflect new ownership
        property.ForSale = false;
        property.OwnerID = offer.UserID;
        _unitOfWork.PropertyRepo.Update(property);

        await _unitOfWork.CommitAsync();
    }
}