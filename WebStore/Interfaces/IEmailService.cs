using System.Net.Mail;

namespace WebStore.Interfaces
{
    public interface IEmailService
    {
        Task Send(int userId, int[] arr);
    }
}
