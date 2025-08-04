using IllustrationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace IllustrationProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>(); 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Books" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 1200, CategoryId = 1 },
                new Product { Id = 2, Name = "Headphones", Price = 150, CategoryId = 1 },
                new Product { Id = 3, Name = "Novel", Price = 25, CategoryId = 2 }
            );
        }
    }
}
