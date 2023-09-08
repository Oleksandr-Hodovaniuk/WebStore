using WebStore.Models.DTOs;
using WebStore.Models.VeiwModels;

namespace WebStore.Interfaces
{
    public interface ILogInService
    {
        Task<UserDTO> LogIn(LoInViewModel model);
    }
}
