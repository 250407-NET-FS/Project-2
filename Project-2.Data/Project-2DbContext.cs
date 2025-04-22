using Microsoft.EntityFrameworkCore;
using Project_2.Models;

namespace Project_2.Data;

public class Project_2DbContext : DbContext
{
    public Project_2DbContext(DbContextOptions<Project_2DbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}