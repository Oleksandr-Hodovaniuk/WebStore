using Microsoft.EntityFrameworkCore;
using Mobileshop.Models;
using WebStore.Data;
using WebStore.Interfaces;
using WebStore.Models;
using WebStore.Models.DTOs;

namespace WebStore.Services
{
    public class CartService : ICartService
    {
        //Add database contex.
        private readonly AppDbContext context;
        public CartService(AppDbContext context)
        {
            this.context = context;
        }

        //Get the user's products.
        public async Task<ICollection<CartItemDTO>> GetCartItems(int userId)
        {
            var cartList = await context.CartItems.Include(c => c.Product).Where(c => c.UserId == userId).ToListAsync();

            var dtoList = new List<CartItemDTO>();

            foreach (var cart in cartList)
            {
                dtoList.Add(CartItemToDTO(cart));
            }

            return dtoList;
        }

        //Create CartItemDTO from CartItem.
        private CartItemDTO CartItemToDTO(CartItem item)
        {
            var dto = new CartItemDTO()
            {
                Product = ProductToDTO(item.Product),
                Quantity = item.Quantity
            };

            return dto;
        }

        //Create ProductDTO from Product.
        private ProductDTO ProductToDTO(Product product)
        {
            var dto = new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = Convert.ToBase64String(product.Image)
            };

            return dto;
        }

        //Delete all the user's cart items.
        public async Task DeleteUserCartItems(int userId)
        {
            var cartItemToDelete = await context.CartItems.Where(c => c.UserId == userId).ToListAsync();

            context.CartItems.RemoveRange(cartItemToDelete);

            await context.SaveChangesAsync();
        }

        //Add cart item to user's shoping cart.
        public async Task AddCartItem(int userId, int productId)
        {
            var cartItem = await context.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = 1
                };

                context.CartItems.Add(cartItem);

                await context.SaveChangesAsync();
            }
            else
            {
                cartItem.Quantity++;

                await context.SaveChangesAsync();
            }
        }

        //Delete one cart item from user's shoping cart.
        public async Task DeleteCartItem(int userId, int productId)
        {
            var cartItem = await context.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null && cartItem.Quantity > 1)
            {
                cartItem.Quantity--;

                await context.SaveChangesAsync();
            }
            else if (cartItem.Quantity == 1)
            {
                context.CartItems.Remove(cartItem);
                await context.SaveChangesAsync();
            }
            else
            {
                return;
            }
        }

        //Get total cart items price.
        public async Task<int> GetTotalPrice(int userId)
        {
            var cartList = await context.CartItems.Include(c => c.Product).Where(c => c.UserId == userId).ToListAsync();

            int prices = 0;

            foreach (var cart in cartList)
            {
                prices += cart.Quantity * cart.Product.Price;
            }

            return prices;
        }

        //Get total selected products price.
        public async Task<int> GetTotalSelectedPrice(int userId, int[] arr)
        {

            int price = 0;

            var cartList = await context.CartItems.Include(c => c.Product).Where(c => c.UserId == userId).ToListAsync();


            foreach (var i in arr)
            {
                foreach (var cart in cartList)
                {
                    if (cart.ProductId == i)
                    {
                        price += cart.Product.Price * cart.Quantity;
                    }
                }
            }

            return price;
        }

        //Purchase products.
        public async Task PurchaseProducts(PurchasedProducts products)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == products.UserId);

            if (user != null)
            {
                var cartItemsList = await context.CartItems.ToListAsync();

                foreach (var id in products.ProductsId)
                {
                    foreach (var cartItem in cartItemsList)
                    {
                        if (cartItem.ProductId == id)
                        {
                            context.CartItems.Remove(cartItem);
                            await context.SaveChangesAsync();
                        }
                    }
                }
            }
        }
    }
}
