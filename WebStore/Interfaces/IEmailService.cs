using System.Net.Mail;
using WebStore.Models;

namespace WebStore.Interfaces
{
    public interface IEmailService
    {
        Task Send(PurchasedProducts products);
    }
}
