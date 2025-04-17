using CTRLE_Handel_oev_identity.Datas;
using CTRLE_Handel_oev_identity.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CTRLE_Handel_oev_identity.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> ProductList { get; set; } = new List<Product>();

        public async Task<IActionResult> SearchBar(string SearchWord)
        {
            if (string.IsNullOrWhiteSpace(SearchWord))
            {
                var showAllProducts = await _context.Products.ToListAsync();
                return View("Index", showAllProducts);
            }

            var searchResult = await _context.Products.Where(p => p.ProductName.ToUpper() == SearchWord.ToUpper() || p.Category.CategoryName.ToUpper() == SearchWord.ToUpper()).ToListAsync();

            if (!searchResult.Any() || searchResult == null)
            {
                return RedirectToAction("Index", "Product");
            }

            return View("Index", searchResult);
        }

        public async Task<IActionResult> Index()
        {
            var ProductList = await _context.Products.ToListAsync();

            if (!ProductList.Any())
            {
                ProductList = new List<Product>
                {
                    new Product{ProductName = "Golfboll TitlesV3", Description = "En fin golfboll", Price = 29.99M, ImageUrl = "product3.png", CategoryId = 4},
                    new Product{ProductName = "Golfboll TitlesV4", Description = "En fin golfboll", Price = 29.99M, ImageUrl = "product4.png", CategoryId = 4},

                    new Product{ProductName = "Tröja blå", Description = "En fin blå tröja", Price = 39.99M, ImageUrl = "product10.png", CategoryId = 2},
                    new Product{ProductName = "Tröja grön", Description = "En fin grön tröja", Price = 39.99M, ImageUrl = "product11.png", CategoryId = 2},

                    new Product{ProductName = "Keps grön", Description = "En fin grön keps", Price = 29.99M, ImageUrl = "product7.png", CategoryId = 2},
                    new Product{ProductName = "Keps blå", Description = "En fin blå keps", Price = 29.99M, ImageUrl = "product8.png", CategoryId = 2}                  
                    
                };

                await _context.Products.AddRangeAsync(ProductList);
                await _context.SaveChangesAsync();
            }

       
            return View(ProductList);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var productById = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (productById == null)
            {
                return NotFound();
            }

            return View(productById);
        }
    }
}
