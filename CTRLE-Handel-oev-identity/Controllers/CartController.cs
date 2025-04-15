using CTRLE_Handel_oev_identity.Areas.Identity.Data;
using CTRLE_Handel_oev_identity.Datas;
using CTRLE_Handel_oev_identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CTRLE_Handel_oev_identity.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUserTable> _userManager;
        public CartController(AppDbContext context, UserManager<IdentityUserTable> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public string ErrorMsg { get; set; }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                TempData["ErrorMsg"] = "Du måste logga in för att lägga till i varukorgen";
                return RedirectToAction("Index", "Product");
            }

            var cartItem = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (cartItem == null)
            {
                TempData["ErrorMsg"] = "Produkten kunde inte hittas";
                return RedirectToAction("Index", "Product");
            }

            var cart = new Cart
            {
                ProductId = cartItem.ProductId,
                Product = cartItem,
                UserId = loggedInUser.Id,
                Quantity = 1,
                ImageUrl = cartItem.ImageUrl
            };

            await _context.Cart.AddAsync(cart);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Product");
        }
    }
}
