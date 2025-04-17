using CTRLE_Handel_oev_identity.Areas.Identity.Data;
using CTRLE_Handel_oev_identity.Datas;
using CTRLE_Handel_oev_identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CTRLE_Handel_oev_identity.Controllers
{
    public class BuyController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUserTable> _userManager;
        public BuyController(AppDbContext context, UserManager<IdentityUserTable> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (loggedInUser == null)
            {
                return RedirectToAction("Index", "Product");
            }

            var cartItems = await _context.Cart
                .Where(c => c.UserId == loggedInUser.Id)
                .Include(p => p.Product)
                .ToListAsync();


            var viewModel = new OrderInfoViewModel
            {
                CartItems = cartItems
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(string fullName, string cardNumber)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (loggedInUser == null) 
            {
                return RedirectToAction("Index", "Product");
            }

            var userCart = await _context.Cart.Where(u => u.UserId == loggedInUser.Id)
                .Include(p => p.Product)
                .ToListAsync();

            if (!userCart.Any())
            {
                return RedirectToAction("Index", "Product");
            }

            var totalAmount = userCart.Sum(item => item.Product.Price * item.Quantity);

            var order = new Order
            {
                UserId = loggedInUser.Id,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>(),
                TotalAmount = totalAmount
            };

            foreach (var item in userCart)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });
            }

            _context.Orders.Add(order);
            _context.Cart.RemoveRange(userCart);
            await _context.SaveChangesAsync();



            return View("Receipt", order); 
        }

        public async Task<IActionResult> Receipt(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return RedirectToAction("Index", "Product"); 
            }

            return View(order);
        }
    }
}
