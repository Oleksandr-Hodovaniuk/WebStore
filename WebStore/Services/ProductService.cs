using Microsoft.EntityFrameworkCore;
using Mobileshop.Models;
using WebStore.Data;
using WebStore.Models.DTOs;
using WebStore.Interfaces;


namespace WebStore.Services
{
    public class ProductService : IProductService
    {
        //Add database context.
        private readonly AppDbContext context;
        public ProductService(AppDbContext context)
        {
            this.context = context;
        }

        //Get all products.
        public async Task<ICollection<ProductDTO>> GetProductsAsync()
        {
            var products = await context.Products.ToListAsync();

            var dtoes = new List<ProductDTO>();

            foreach (var product in products) 
            {
                dtoes.Add(ProductToDTO(product));
            }

            return dtoes;
        }

        //Create ProductDTO object from Product.
        private ProductDTO ProductToDTO(Product product)
        {
            var dto = new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image
            };

            return dto;
        }
    }
}
