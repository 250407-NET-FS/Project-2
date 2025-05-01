using System.Transactions;

namespace Project_2.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IOfferRepository _offerRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    private TransactionScope? _transaction;

    public UnitOfWork(IFavoriteRepository favRepo, IOfferRepository offerRepo,
                      IPropertyRepository propRepo, IPurchaseRepository purRepo) {
        _favoriteRepository = favRepo;
        _offerRepository = offerRepo;
        _propertyRepository = propRepo;
        _purchaseRepository = purRepo;
    }

    // Public getters for repositories
    public IFavoriteRepository FavoriteRepo => _favoriteRepository;
    public IOfferRepository OfferRepo => _offerRepository;
    public IPropertyRepository PropertyRepo => _propertyRepository;
    public IPurchaseRepository PurchaseRepo => _purchaseRepository;

    public void BeginTransaction()
    {
        _transaction = new TransactionScope();
    }

    public async Task CommitAsync()
    {
        try
        {
            // may appear to save favoritesRepository only but in the background 
            // it's calling dbContext.save and is saving changes to all repos
            await _favoriteRepository.SaveChangesAsync();
            _transaction!.Complete();
        }
        catch
        {
            throw new Exception("Transaction rolled back, some records failed!");
        }
    }
}