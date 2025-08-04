using IllustrationProject.Data;
using IllustrationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace IllustrationProject.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context) => _context = context;

        public async Task<IEnumerable<object>> GetExpensiveRawAsync(double minPrice)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.Price >= minPrice)
                .OrderByDescending(p => p.Price)
                .Skip(0).Take(100)
                .Select(p => new
                {
                    p.Name,
                    p.Price
                })
                .ToListAsync();
        }


        public async Task<List<ProductDto>> GetProductsWithCategoriesAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Select(p => new ProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.Category != null ? p.Category.Name : null
                })
                .ToListAsync();
        }

        public async Task BulkInsertAsync(IEnumerable<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }
    }
}
