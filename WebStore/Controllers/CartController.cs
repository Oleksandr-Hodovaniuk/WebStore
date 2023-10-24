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
        [HttpGet("Get/{userId}")]
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
        [HttpDelete("Delete/{userId}")]
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

        //Add cart item to user's shoping cart.
        [HttpPost("Add/{userId}/{productId}")]
        public async Task<IActionResult> AddCartItem(int userId, int productId)
        {
            try 
            {
                await cartService.AddCartItem(userId, productId);
                return Ok("Product was added to shoping cart.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Delete cart item from user's shoping cart.
        [HttpDelete("Delete/{userId}/{productId}")]
        public async Task<IActionResult> DeleteCartItem(int userId, int productId)
        {
            try
            {
                await cartService.DeleteCartItem(userId, productId);

                return Ok("Product was deleted from shoping cart.");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        //Get total cart items price.
        [HttpGet("Price/{userId}")]
        public async Task<ActionResult<int>> GetTotalPrice(int userId)
        {
            try
            {
                return Ok(await cartService.GetTotalPrice(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Get total selected products price.
        [HttpGet("Price2")]
        public async Task<ActionResult<int>> GetTotalSelectedPrice([FromQuery] int[] arr)
        {
            try
            {
                return Ok(await cartService.GetTotalSelectedPrice(arr));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
