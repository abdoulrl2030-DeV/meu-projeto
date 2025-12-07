using Microsoft.EntityFrameworkCore;
using MeuProjeto.Api.Models;

namespace MeuProjeto.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
    }
}
