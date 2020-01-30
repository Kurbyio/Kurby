using Microsoft.EntityFrameworkCore;

namespace Kurby.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users {get; set;}
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Handles duplicate entry for email in user table
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}