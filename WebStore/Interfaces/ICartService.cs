﻿using WebStore.Models.DTOs;

namespace WebStore.Interfaces
{
    public interface ICartService
    {
        //Get all the user's cart items.
        Task<ICollection<CartItemDTO>> GetCartItems(int userId);

        //Delete all the user's cart items.
        Task DeleteUserCartItems(int userId);
    }
}
