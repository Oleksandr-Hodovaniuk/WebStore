using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.Models.DTOs;
using WebStore.Models.VeiwModels;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        //Add ILogInService.
        private readonly ILogInService logInService;
        public LogInController(ILogInService logInService)
        {
            this.logInService = logInService;
        }

        //Log in method.
        [HttpPost]
        public async Task<ActionResult<UserDTO>> LogIn(LogInViewModel model)
        {
            try
            {
                var user = await logInService.LogIn(model);

                if (user != null)
                    return user;

                return BadRequest("Incorrect username or password.");
            }
            catch (Exception ex) 
            {
                BadRequest(ex.Message);
            }

            return BadRequest();
        }
    }
}
