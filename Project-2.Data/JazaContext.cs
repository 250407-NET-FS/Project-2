using Microsoft.EntityFrameworkCore;
using Project_2.Models;

namespace Project_2.Data;

public class JazaContext : DbContext
{
    public JazaContext(DbContextOptions<JazaContext> options) : base(options) { }

    public DbSet<User> Credential { get; set; }
    public DbSet<User> Favorite { get; set; }
    public DbSet<User> Offer { get; set; }
    public DbSet<User> Property { get; set; }
    public DbSet<User> Purchase { get; set; }
    public DbSet<User> User { get; set; }
}