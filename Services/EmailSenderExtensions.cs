using OnBazar.Services;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace OnBazar.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link, string hara)
        {
            link = link.Replace("api/Account", "account");
            if (hara == "E")
            {                
                return emailSender.SendEmailAsync(email, "Confirm your email",
                    $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
            }
            else if (hara == "Rp")
            {                
                return emailSender.SendEmailAsync(email, "Reset Password",
                    $"Please reset your password by clicking this here link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
            }
            else
            {
                return emailSender.SendEmailAsync(email, "Reset Password",
                    $"Please reset your password by clicking here: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
            }
        }
    }
}
