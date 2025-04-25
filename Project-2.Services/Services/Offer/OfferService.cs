using Microsoft.EntityFrameworkCore;
using Project_2.Data;
using Project_2.Models;

namespace Project_2.Services;

public class OfferService : IOfferService
{
    private readonly IOfferRepository _offerRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;

    public OfferService(IOfferRepository offerRepository, IPropertyRepository propertyRepository, IUserRepository userRepository)
    {
        _offerRepository = offerRepository;
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Offer>> GetAllAsync()
    {
        return await _offerRepository.GetAllAsync();
    }

    public async Task<Offer?> GetByIdAsync(Guid id)
    {
        return await _offerRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Offer offer)
    {
        // check if offer is good
        if (offer is null)
            throw new Exception("offer cannot be null");

        // add to database
        await _offerRepository.AddAsync(offer);
        // save database
        await _offerRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Offer>> GetAllForProperty(Guid propertyId)
    {
        // check if property exist
        Property property = await _propertyRepository.GetByIdAsync(propertyId);
        if (property is null)
            throw new Exception("Property doees not exist");

        // return list of offers for property
        return await _offerRepository.GetAllForProperty(propertyId);
    }


    public async Task<IEnumerable<Offer>> GetAllByUser(Guid userId)
    {
        // check if user exist
        User user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new Exception("User does not exist.");

        // return list of offers made by user
        return await _offerRepository.GetAllByUser(userId);
    }
}