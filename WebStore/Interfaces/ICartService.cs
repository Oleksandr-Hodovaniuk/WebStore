using WebStore.Models.DTOs;

namespace WebStore.Interfaces
{
    public interface ICartService
    {
        Task<ICollection<CartItemDTO>> GetCartItems(int userId); 
    }
}
