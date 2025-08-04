using IllustrationProject.Models;

namespace IllustrationProject.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsWithCategoriesAsync();
        Task BulkInsertAsync(IEnumerable<Product> products);
        Task<IEnumerable<object>> GetExpensiveRawAsync(double minPrice);

    }

    public class ProductDto
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? CategoryName { get; set; }
    }
}
