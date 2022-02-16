using Microsoft.AspNetCore.Mvc;

namespace PaginationNET6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<List<Product>>> GetProduct(int page)
        {
            if (_context.products == null)
            {
                return NotFound();
            }

            var pageResults = 3f; //Total data per page
            var pageCount = Math.Ceiling(_context.products.Count() / pageResults);

            var product = await _context.products
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var response = new ProductResponse
            {
                Products = product,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }
    }
}
