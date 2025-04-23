using Microsoft.EntityFrameworkCore;
using Project_2.Data;
using Project_2.Models;

namespace Project_2.Services.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly JazaContext _context;

    public PurchaseService(IPurchaseRepository purchaseRepository, JazaContext context)
    {
        _purchaseRepository = purchaseRepository;
        _context = context;
    }

    public async Task<IEnumerable<Purchase>> GetAllAsync()
    {
        return await _purchaseRepository.GetAllAsync();
    }

    public async Task<Purchase?> GetByIdAsync(Guid id)
    {
        return await _purchaseRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Purchase purchase)
    {
        await _purchaseRepository.AddAsync(purchase);
        await _purchaseRepository.SaveChangesAsync();
    }
}