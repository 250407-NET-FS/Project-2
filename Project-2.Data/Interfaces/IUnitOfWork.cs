namespace Project_2.Data;

public interface IUnitOfWork
{
    IFavoriteRepository FavoriteRepo { get; }
    IOfferRepository OfferRepo { get; }
    IPropertyRepository PropertyRepo { get; }
    IPurchaseRepository PurchaseRepo { get; }

    public void BeginTransaction();
    public Task CommitAsync();
}