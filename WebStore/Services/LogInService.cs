using Microsoft.EntityFrameworkCore;
using Mobileshop.Models;
using WebStore.Data;
using WebStore.Interfaces;
using WebStore.Models.DTOs;
using WebStore.Models.VeiwModels;

namespace WebStore.Services
{
    public class LogInService : ILogInService
    {
        //Add database context.
        private readonly AppDbContext context;
        public LogInService(AppDbContext context)
        {
            this.context = context;
        }

        //Log in user.
        public async Task<UserDTO> LogIn(LogInViewModel model)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Name == model.Name && u.Password == model.Password);
  
            if (user != null)
            {
                return UserToDTO(user);
            }

            UserDTO? dto = null;

            return dto!;
        }

        //Create UserDTO object from User.
        private UserDTO UserToDTO(User user)
        {
            var dto = new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };

            return dto;
        }
    }
}
