using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mobileshop.Models;
using WebStore.Data;
using WebStore.Interfaces;
using WebStore.Models.DTOs;
using WebStore.Models.VeiwModels;
using WebStore.Validators;

namespace WebStore.Services
{
    public class RegisterService : IRegisterService
    {
        //Add validator.
        private readonly RegisterViewModelValidator validator;

        //Add database context.
        private readonly AppDbContext context;
        public RegisterService(RegisterViewModelValidator validator, AppDbContext context)
        {
            this.validator = validator;
            this.context = context;
        }

        //Register user.
        public async Task<ValidationResult> Register(RegisterViewModel model)
        {
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                return validationResult;

            validationResult = await IsUnique(model);

            if(!validationResult.IsValid)
                return validationResult;

            var user = ViewModelToUser(model);

            if (user != null) 
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }

            return validationResult;
        }

        //Check if the user is unique.
        private async Task<ValidationResult> IsUnique(RegisterViewModel model)
        {
            var validationResult = new ValidationResult();

            if (await context.Users.AnyAsync(u => u.Name == model.Name))
            {
                validationResult.Errors.Add(new ValidationFailure()
                { 
                    PropertyName = "Name", ErrorMessage = "This username is already in use." 
                });
            }

            if (await context.Users.AnyAsync(u => u.Email == model.Email))
            {
                validationResult.Errors.Add(new ValidationFailure() 
                {
                    PropertyName = "Email", ErrorMessage = "This email is already in use."
                });
            }
             
            return validationResult;
        }

        //Create User from RegisterViewModel.
        private User ViewModelToUser(RegisterViewModel model)
        {
            var user = new User() 
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password
            };

            return user;
        }

        //Create UserDTO from User.
        public async Task<UserDTO> GetUserDTO(string name)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Name == name);

            UserDTO? dto = null;

            if (user != null)
                
            dto = new UserDTO() 
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };

            return dto!;
        }
    }
}
