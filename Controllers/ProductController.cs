using IllustrationProject.Models;
using IllustrationProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace IllustrationProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) => _productService = productService;

        [HttpGet("expensive")]
        public async Task<IActionResult> GetExpensive([FromQuery] double minPrice = 100)
            => Ok(await _productService.GetExpensiveRawAsync(minPrice));

        [HttpGet("with-categories")]
        public async Task<IActionResult> GetWithCategories()
            => Ok(await _productService.GetProductsWithCategoriesAsync());

        [HttpPost("bulk-insert")]
        public async Task<IActionResult> BulkInsert([FromBody] List<Product> products)
        {
            await _productService.BulkInsertAsync(products);
            return Ok();
        }
    }
}
