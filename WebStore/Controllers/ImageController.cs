using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [HttpGet("{imageName}")]
        public async Task<IActionResult> GetImage(string imageName) 
        {
            string imagePath = Path.Combine("wwwroot/images/", imageName);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound("There is no image with this name.");
            }

            var image = await System.IO.File.ReadAllBytesAsync(imagePath);

            return File(image, "image/png");
        }
    }
}
