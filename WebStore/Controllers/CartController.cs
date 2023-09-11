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

        //Delete all the user's cart items.
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserCartItems(int userId)
        {
            try
            {
                await cartService.DeleteUserCartItems(userId);

                return Ok("User's cart was cleared.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
