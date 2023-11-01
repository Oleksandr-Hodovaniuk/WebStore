using WebStore.Data;
using WebStore.Interfaces;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using WebStore.Models;
using WebStore.Models.DTOs;
using Mobileshop.Models;
using System.Text;
using Microsoft.AspNetCore.Hosting.Server;

namespace WebStore.Services
{
    public class EmailService : IEmailService
    {
        //Add database context.
        private readonly AppDbContext context;
        
        //Add cart service.
        private readonly ICartService cartService;
        public EmailService(AppDbContext context, ICartService cartService)
        {
            this.context = context;
            this.cartService = cartService;
        }

        //Send email to user.
        public async Task Send(PurchasedProducts products)
        {
            var user = await context.Users.FirstOrDefaultAsync(c => c.Id == products.UserId);

            var cartItems = await context.CartItems.ToListAsync();
            var productList = await context.Products.ToListAsync();
            var purchaseItems = new List<CartItem>();
            var urlList = new List<string>()
            {
                "https://content1.rozetka.com.ua/goods/images/big/284920851.jpg",
                "https://content1.rozetka.com.ua/goods/images/big_tile/221026603.jpg",
                "https://content.rozetka.com.ua/goods/images/big/310649358.jpg",
                "https://content.rozetka.com.ua/goods/images/big/282291399.jpg",
                "https://content1.rozetka.com.ua/goods/images/big/319594401.jpg",
                "https://content2.rozetka.com.ua/goods/images/big/284920878.jpg"
            };

            foreach (var i in products.ProductsId)
            {
                foreach (var cartItem in cartItems)
                {
                    if (i == cartItem.ProductId)
                    {
                        purchaseItems.Add(cartItem);
                    }
                }
            }

            if (user != null)
            {
                var price = await cartService.GetTotalSelectedPrice(products.UserId, products.ProductsId);

                try
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Admin", "mobileshop@gmail.com"));
                    message.To.Add(new MailboxAddress("Client", "s.godovanuk@gmail.com"));               
                 
                    string htmlContent = @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <style>
        .mainDiv{
            display: block;
            margin-top: 75px;
            padding-bottom: 50px;
        }
        .cartItem {
            margin-top: 10px;
            display: flex;
        }
        .product {
            width: 300px;
            border: 2px solid #13276A;
            border-radius: 10px;
        }
        .verticalContainer {
            display: block;
            flex-direction: column;
            align-items: center;
        }
        .image {
            height: 160px;
            margin-top: 5px;
        }
        .name{   
            font-size: 10px;
            margin-top: 5px;
            width: auto;
            height: auto;
            text-align: center;
            word-wrap: break-word;
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
        }
        .description {
            font-size: 10px;
            margin-top: 5px;
            width: auto;
            height: auto;
            text-align: left;
            word-wrap: break-word;
            font-family: Arial, Helvetica, sans-serif
        }
        .price {
            font-size: 10px;
            margin-top: 5px;
            width: auto;
            height: auto;
            color: red;
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
        }
        .productFunc{
            display: flex;
            align-items: center;
            padding: 30px;
            width: auto;
            height: 100px;
        }
        .quantity {
            font-size: 28px;
            color: black;
            margin-left: 10px;
            font-family: Arial, Helvetica, sans-serif;
        }
    </style>
</head>
<body>
<div class=""mainDiv"">";

                    var builder = new BodyBuilder();
                    int quantity = 0;
                    foreach (var cart in purchaseItems)
                    {
                        htmlContent += CreateProductHtml(productList.FirstOrDefault(p => p.Id == cart.ProductId),
                            cart.Quantity,
                            urlList[cart.ProductId - 1]);

                        quantity += cart.Quantity;
                    }
                    htmlContent += "</div></body></html>";

                    message.Subject = $"Congratulations, {user.Name}, on the successfull purchase of our product(s) " +
                        $"in our web store! Number of your products: {quantity}; Sum of your products: {price} ₴";
                    builder.HtmlBody = htmlContent;

                    message.Body = builder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 587, false);
                        await client.AuthenticateAsync("s.godovanuk@gmail.com", "mzpr uytm gavw ebwb");
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }

                    await cartService.PurchaseProducts(products);

                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Email sending failed. Error: {ex.Message}");
                }
            }
            else 
            {
                Console.WriteLine("Error!");
            }  
        }

        private string CreateProductHtml(Product product, int quantity, string url)
        {
            string productHtml = @$"
    <div class=""cartItem"">
        <div class=""product"">
            <div style=""display: flex; flex-direction: column; align-items: center;"">
                <img src=""{url}"" class=""image"" alt=""product image""/>
                <div class=""name"">{product.Name}</div>
                <div class=""description"">{product.Description}</div>
                <div class=""price"">{product.Price} ₴</div>
            </div>
        </div>
        <div class=""productFunc"">
            <span class=""quantity"">x {quantity}</span>
        </div>
    </div>";

            return productHtml;
        }
    }
}
