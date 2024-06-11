
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace CI_Platform_Backend_Services.Email;

public class EmailService : IEmailService
{
    private readonly string _url;
    private readonly IConfiguration _configuration;
    public EmailService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _configuration = configuration;
        _url = httpContextAccessor.HttpContext.Request.Scheme + ".//" + httpContextAccessor.HttpContext.Request.Host;
    }

    public async Task<bool> SendResetPasswordAsync(string email, string token)
    {
        if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
        {
            return false;
        }
        string subject = "Reset Password - CI Platform";
        string message = "Tap on the given link below to open reset password page. \n\n\n Link: " + "http://localhost:4200/" + "/reset-password?token=" + token;
        string mail = _configuration["EmailCredentials:Email"] ?? "";
        string password = _configuration["EmailCredentials:Password"] ?? "";

        SmtpClient client = new("smtp.office365.com")
        {
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(mail, password)
        };
        await client.SendMailAsync(new MailMessage(from: mail ?? "", to: email ?? "", subject, message));

        return true;
    }


}
