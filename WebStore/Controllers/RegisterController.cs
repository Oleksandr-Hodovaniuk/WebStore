using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.Models.VeiwModels;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        //Add IRegisterService.
        private readonly IRegisterService registerService;
        public RegisterController(IRegisterService registerService)
        {
            this.registerService = registerService;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                var validationResult = await registerService.Register(model);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                return Ok(await registerService.GetUserDTO(model.Name));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
