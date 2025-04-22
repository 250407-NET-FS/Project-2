using Microsoft.EntityFrameworkCore;
using Project_2.Models;

namespace Project_2.Data;

public class JazaContext : DbContext
{
    public JazaContext(DbContextOptions<JazaContext> options) : base(options) { }

    public DbSet<Credential> Credential { get; set; }
    public DbSet<Favorite> Favorite { get; set; }
    public DbSet<Offer> Offer { get; set; }
    public DbSet<Property> Property { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<User> User { get; set; }
}