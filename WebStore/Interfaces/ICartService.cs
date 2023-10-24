using WebStore.Models.DTOs;

namespace WebStore.Interfaces
{
    public interface ICartService
    {
        //Get all the user's cart items.
        Task<ICollection<CartItemDTO>> GetCartItems(int userId);

        //Delete all the user's cart items.
        Task DeleteUserCartItems(int userId);

        //Add cart item to user's shoping cart.
        Task AddCartItem(int userId, int productId);

        //Delete one cart item from user's shoping cart.
        Task DeleteCartItem(int userId, int productId);

        //Get total cart items price.
        Task<int> GetTotalPrice(int userId);

        //Get total selected products price.
        Task<int>GetTotalSelectedPrice(int[] productsId);
    }
}
