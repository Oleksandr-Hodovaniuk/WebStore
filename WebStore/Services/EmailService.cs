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

            if (user != null)
            {
                var price = await cartService.GetTotalSelectedPrice(products.UserId, products.ProductsId);

                try
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Admin", "mobileshop@gmail.com"));
                    message.To.Add(new MailboxAddress("Client", "s.godovanuk@gmail.com"));
                    message.Subject = $"Congratulations, {user.Name}, on the successful purchase of our product(s) in our web store! Sum of your purshase: {price} ₴";

                    //string htmlContent = File.ReadAllText("email.html");

                    var builder = new BodyBuilder();
                    builder.HtmlBody = "<h1>Test</h1>";

                    message.Body = builder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 587, false);
                        await client.AuthenticateAsync("s.godovanuk@gmail.com", "ypdn ynsz zogg nywm");
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }

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
    }
}
