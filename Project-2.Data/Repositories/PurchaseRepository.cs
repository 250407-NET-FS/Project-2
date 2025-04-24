using Microsoft.EntityFrameworkCore;
using Project_2.Models;

namespace Project_2.Data;

public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository {
    public PurchaseRepository(JazaContext context) : base(context) {}
}