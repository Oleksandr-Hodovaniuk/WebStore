using WebStore.Models.DTOs;

namespace WebStore.Interfaces
{
    public interface IProductService
    {
        Task<ICollection<ProductDTO>> GetProductsAsync();
    }
}
