using Microsoft.EntityFrameworkCore;
using Mobileshop.Models;

namespace Web_Store.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartItem>()
                .HasKey(ci => new { ci.UserId, ci.ProductId });

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            //Add some products to database.
            byte[] imageBytes;

            using (FileStream fs = new FileStream("Data//Images//Samsung Galaxy A24 6128GB Black.png", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    imageBytes = br.ReadBytes((int)fs.Length);
                }
            }

            Product product1 = new Product()
            {
                Id = 1,
                Name = "Samsung Galaxy A24 6128GB Black",
                Description = "Screen (6.5\", Super AMOLED, 2340x1080) / Mediatek Helio G99 (2 x 2.6 GHz + 6 x 2.0 GHz) / main triple camera: 50 MP + 5 MP + 2 MP, front camera: 13 MP / RAM 6 GB / 128 GB of built-in memory + microSD (up to 1 TB) / 3G / LTE / GPS / GLONASS / BDS / support for 2x SIM cards (Nano-SIM) / Android 13 / 5000 mA * h.",
                Price = 8999,
                Image = imageBytes
            };

            using (FileStream fs = new FileStream("Data//Images//Apple iPhone 13 128GB Midnight.png", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    imageBytes = br.ReadBytes((int)fs.Length);
                }
            }

            Product product2 = new Product()
            {
                Id = 2,
                Name = "Apple iPhone 13 128GB Midnight",
                Description = "Screen (6.1\", OLED (Super Retina XDR), 2532x1170) / Apple A15 Bionic / dual main camera: 12 MP + 12 MP, front camera: 12 MP / 128 GB of built-in memory / 3G / LTE / 5G / GPS / Nano-SIM, eSIM / iOS 15.",
                Price = 33499,
                Image = imageBytes
            };

            using (FileStream fs = new FileStream("Data//Images//Samsung Galaxy S23 Ultra 12-512GB Green.png", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    imageBytes = br.ReadBytes((int)fs.Length);
                }
            }

            Product product3 = new Product()
            {
                Id = 3,
                Name = "Samsung Galaxy S23 Ultra 12-512GB Greent",
                Description = "Screen (6.8\", Dynamic AMOLED 2X, 3088x1440) / Qualcomm Snapdragon 8 Gen 2 for Galaxy (3.36 GHz + 2.8 GHz + 2.8 GHz + 2.0 GHz) / main quad camera: 200 MP + 12 MP + 10 MP + 10 MP, front 12 MP / RAM 12 GB / 512 GB built-in memory / 3G / LTE / 5G / GPS / support for 2 SIM cards (Nano-SIM) / Android 13 / 5000 mAh.",
                Price = 55799,
                Image = imageBytes
            };

            using (FileStream fs = new FileStream("Data//Images//Motorola G32-6-128GB Satin Maroon.png", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    imageBytes = br.ReadBytes((int)fs.Length);
                }
            }

            Product product4 = new Product()
            {
                Id = 4,
                Name = "Motorola G32-6-128GB Satin Maroon",
                Description = "Screen (6.5\", LCD, 2400x1080) / Qualcomm Snapdragon 680 (2.4 GHz) / main triple camera: 50 MP + 8 MP + 2 MP, front camera: 16 MP / RAM 6 GB / 128 GB built-in memory + microSD (up to 1 TV) / 3G / LTE / GPS / support for 2 SIM cards (Nano-SIM) / Android 12 / 5000 mAh.",
                Price = 6399,
                Image = imageBytes
            };

            using (FileStream fs = new FileStream("Data//Images//Samsung Galaxy A34 8-256GB Light Green.png", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    imageBytes = br.ReadBytes((int)fs.Length);
                }
            }

            Product product5 = new Product()
            {
                Id = 5,
                Name = "Samsung Galaxy A34 8-256GB Light Green",
                Description = "Screen (6.6\", Super AMOLED, 2340x1080) / Mediatek Dimensity 1080 (2 x 2.6 GHz + 6 x 2.0 GHz) / main triple camera: 48 MP + 8 MP + 5 MP, front camera: 13 MP / RAM 8 GB / 256 GB of built-in memory + microSD (up to 1 TB) / 3G / LTE / 5G / GPS / A-GPS / GLONASS / BDS / support for 2 SIM cards (Nano-SIM) / Android 13 / 5000 mAh * h.",
                Price = 13999,
                Image = imageBytes
            };

            using (FileStream fs = new FileStream("Data//Images//Apple iPhone 14 Pro Max 128GB Deep-Purple.png", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    imageBytes = br.ReadBytes((int)fs.Length);
                }
            }

            Product product6 = new Product()
            {
                Id = 6,
                Name = "Apple iPhone 14 Pro Max 128GB Deep-Purple",
                Description = "Screen (6.7\", OLED (Super Retina XDR), 2796x1290) / Apple A16 Bionic / main quad camera: 48 MP + 12 MP + 12 MP + 12 MP, front camera: 12 MP / 128 GB of built-in memory / 3G / LTE / 5G / GPS / Nano-SIM / iOS 16.",
                Price = 54999,
                Image = imageBytes
            };

            //Add some users to database.
            User user1 = new User()
            {
                Id = 1,
                Name = "Robert",
                Email = "robert@gmail.com",
                Password = "robertpassword"
            };

            User user2 = new User()
            {
                Id = 2,
                Name = "William",
                Email = "william@gmail.com",
                Password = "williampassword"
            };

            User user3 = new User()
            {
                Id = 3,
                Name = "Christopher",
                Email = "christopher@gmail.com",
                Password = "christopherpassword"
            };

            //Add some cart items to database.
            CartItem cartItem1 = new CartItem()
            {
                UserId = 1,
                ProductId = 1,
                Quantity = 2,
            };

            CartItem cartItem2 = new CartItem()
            {
                UserId = 2,
                ProductId = 2,
                Quantity = 1,
            };

            CartItem cartItem3 = new CartItem()
            {
                UserId = 3,
                ProductId = 3,
                Quantity = 1,
            };

            CartItem cartItem4 = new CartItem()
            {
                UserId = 2,
                ProductId = 4,
                Quantity = 1,
            };

            CartItem cartItem5 = new CartItem()
            {
                UserId = 3,
                ProductId = 5,
                Quantity = 1,
            };

            modelBuilder.Entity<Product>().HasData(product1, product2, product3, product4, product5, product6);
            modelBuilder.Entity<User>().HasData(user1, user2, user3);
            modelBuilder.Entity<CartItem>().HasData(cartItem1, cartItem2, cartItem3, cartItem4, cartItem5);
        }
    }
}
