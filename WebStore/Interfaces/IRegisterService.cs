using FluentValidation.Results;
using WebStore.Models.DTOs;
using WebStore.Models.VeiwModels;

namespace WebStore.Interfaces
{
    public interface IRegisterService
    {
        Task<ValidationResult> Register(RegisterViewModel model);
        Task<UserDTO> GetUserDTO(string name);
    }
}
