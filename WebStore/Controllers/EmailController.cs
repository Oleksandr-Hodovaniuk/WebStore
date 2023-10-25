using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Macs;
using WebStore.Interfaces;
using WebStore.Models;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    //Register ICartService.
    private readonly IEmailService emailService;
    public EmailController(IEmailService emailService)
    {
        this.emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> Send([FromBody] Email email)
    {
        try
        {
            await emailService.Send(email);

            return Ok("Your purchase was successful, check your gmail.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
       
}