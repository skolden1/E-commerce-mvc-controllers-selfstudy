using CTRLE_Handel_oev_identity.Areas.Identity.Data;
using CTRLE_Handel_oev_identity.Datas;
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


            return View(cartItems);
        }
    }
}
