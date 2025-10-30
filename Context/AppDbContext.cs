using Microsoft.EntityFrameworkCore;
using InstMiggD.Entities;

namespace InstMiggD.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
    }
}