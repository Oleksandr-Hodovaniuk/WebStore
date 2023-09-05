namespace Mobileshop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description{ get; set; }
        public int Price { get; set; }
        public byte[] Image { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

    }
}
