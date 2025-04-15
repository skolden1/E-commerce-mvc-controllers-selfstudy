using CTRLE_Handel_oev_identity.Datas;
using Microsoft.AspNetCore.Mvc;

namespace CTRLE_Handel_oev_identity.Controllers
{
    public class CartController
    {
        private readonly AppDbContext _context;
        public CartController(AppDbContext context)
        {
            _context = context;
        }


        //public async Task<IActionResult> AddToCart(int productId)
        //{
            
        //}
    }
}
