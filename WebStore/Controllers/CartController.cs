using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mobileshop.Models;
using WebStore.Interfaces;
using WebStore.Models.DTOs;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        //Register ICartService.
        private readonly ICartService cartService;
        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        //Get the user's products.
        [HttpGet]
        public async Task<ActionResult<ICollection<CartItemDTO>>> GetCartItems(int userId)
        {
            try
            {
                return Ok(await cartService.GetCartItems(userId));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
