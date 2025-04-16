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

        //onget
        public async Task<IActionResult> ViewCart()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (loggedInUser == null)
            {
                ErrorMsg = "Du måste logga in för att se din kundvagn";
                return View();
            }

            var cartList = await _context.Cart
                .Include(p => p.Product)
                .Where(u => u.UserId == loggedInUser.Id)
                .ToListAsync();

            if (!cartList.Any())
            {
                ErrorMsg = "Din varukorg är tom";
                return View();
            }

            decimal totalPrice = cartList.Sum(item => item.Product.Price * item.Quantity);

            
            ViewBag.TotalPrice = totalPrice;

            return View(cartList);
        }


        //onpost
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

            var checkIfSameProduct = await _context.Cart.FirstOrDefaultAsync(p => p.ProductId == productId && p.UserId == loggedInUser.Id);

            if (checkIfSameProduct != null)
            {
                checkIfSameProduct.Quantity++;
                _context.Cart.Update(checkIfSameProduct);
            }
            else
            {
                var cart = new Cart
                {
                    ProductId = cartItem.ProductId,
                    Product = cartItem,
                    UserId = loggedInUser.Id,
                    Quantity = 1,
                    ImageUrl = cartItem.ImageUrl
                };
                await _context.Cart.AddAsync(cart);
            }

               

            
            await _context.SaveChangesAsync();

            TempData["SuccessMsg"] = "Produkten har lagts till i kundvagnen!";

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity(int productId)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                TempData["ErrorMsg"] = "Du måste logga in för att lägga till i varukorgen";
                return RedirectToAction("Index", "Product");
            }

            var itemToDecrease = await _context.Cart.FirstOrDefaultAsync(u => u.UserId == loggedInUser.Id && u.ProductId == productId);
            if (itemToDecrease == null)
            {
                TempData["ErrorMsg"] = "Produkten hittades inte.";
                return RedirectToAction("Index", "Product");
            }

            if (itemToDecrease.Quantity > 1)
            {
                itemToDecrease.Quantity--;
            }
            else
            {
                _context.Cart.Remove(itemToDecrease);
            }
            await _context.SaveChangesAsync();

            TempData["SuccessMsg"] = "Produkten uppdaterades.";
            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity(int productId)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                TempData["ErrorMsg"] = "Du måste logga in för att lägga till i varukorgen";
                return RedirectToAction("Index", "Product");
            }

            var itemToIncrease = await _context.Cart.FirstOrDefaultAsync(u => u.UserId == loggedInUser.Id && u.ProductId == productId);

            if (itemToIncrease == null)
            {
                TempData["ErrorMsg"] = "Produkten kunde inte hittas";
                return RedirectToAction("Index", "Product");
            }
                     
            itemToIncrease.Quantity++;
            await _context.SaveChangesAsync();

            TempData["SuccessMsg"] = "Produkten uppdaterades.";
            return RedirectToAction("ViewCart");

        }
    }
}
