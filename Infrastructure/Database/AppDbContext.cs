using Microsoft.EntityFrameworkCore;
using MiniDevTo.Domain.Entities;

namespace MiniDevTo.Infrastructure.Database
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options) : base(options) {}
        public DbSet<User> Users => Set<User>();
        public DbSet<Article> Articles => Set<Article>();
        public DbSet<Comment> Comments => Set<Comment>();
    }
}
