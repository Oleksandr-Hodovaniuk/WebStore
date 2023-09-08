using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.Models.DTOs;
using WebStore.Services;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //Register Service.
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        //Get all products.
        [HttpGet]
        public async Task<ActionResult<ICollection<ProductDTO>>> GetProducts()
        {
            try 
            {
                return Ok(await productService.GetProductsAsync());
            }
            catch (Exception ex) 
            {
                BadRequest(ex.Message);
            }

            return BadRequest();
        }
    }
}
