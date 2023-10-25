using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Macs;
using WebStore.Interfaces;

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
    public async Task<IActionResult> Send([FromQuery] int useId, [FromQuery] int[] arr)
    {
        try
        {
            await emailService.Send(useId, arr);

            return Ok("Your purchase was successful, check your gmail.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
       
}