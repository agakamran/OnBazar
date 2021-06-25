using Microsoft.Extensions.Options;
using OnBazar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnBazar.Services
{
    public class EmailSettings
    {
        public String PrimaryDomain { get; set; }

        public int PrimaryPort { get; set; }

        public String SecondayDomain { get; set; }

        public int SecondaryPort { get; set; }

        public String UsernameEmail { get; set; }

        public String UsernamePassword { get; set; }

        public String FromEmail { get; set; }

        public String ToEmail { get; set; }

        public String CcEmail { get; set; }
        public bool UseSsl { get; set; }
        public bool WriteAsFile { get; set; }       

    }
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
    public class EmailSender : IEmailSender, ISmsSender
    {
        
        public EmailSettings _emailSettings { get; }
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;            
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }
        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email)
                                 ? _emailSettings.ToEmail
                                 : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Ağakişiyev kamran Qələndər")
                };
                mail.To.Add(new MailAddress(toEmail));
                //   mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = "Personal Management System - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                //do something here
            }
        }
        public Task SendSmsAsync(string number, string message)
        {
            return Task.FromResult(0);
        }
    }
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, shipper shipDetails);
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        private readonly IRepository<orderm> _orderm = null;
        private readonly IRepository<orderdet> _orderd = null;
        public EmailOrderProcessor(EmailSettings settings, IRepository<orderm> orderm, IRepository<orderdet> orderd)
        {
            emailSettings = settings;
            _orderm = orderm;
            _orderd = orderd;
        }
        public void ProcessOrder(Cart cart, shipper shippingInfo)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.PrimaryDomain;
                smtpClient.Port = emailSettings.PrimaryPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.UsernameEmail, emailSettings.UsernamePassword);
                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = path;// emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder()
                .AppendLine("A new order has been submitted")
                .AppendLine("---")
                .AppendLine("Items:");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.sell_unitprice * line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity,
                    line.Product.prodname, subtotal);
                }            
                body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
                .AppendLine("---")
                .AppendLine("Ship to:")
               // .AppendLine(shippingInfo.client_name)
                .AppendLine(shippingInfo.shipsity)
                .AppendLine(shippingInfo.shipstrit ?? "")
                .AppendLine(shippingInfo.shiphouse ?? "")
               // .AppendLine(shippingInfo.shipflat)
                .AppendLine(shippingInfo.shipphone ?? "")
               // .AppendLine(shippingInfo.Country)
               // .AppendLine(shippingInfo.Zip)
                .AppendLine("---")
                .AppendFormat("Gift wrap: {0}", shippingInfo.GiftWrap ? "Yes" : "No");
                MailMessage mailMessage = new MailMessage(
                emailSettings.FromEmail, // From
                emailSettings.ToEmail, // To
                "New order submitted!", // Subject
                body.ToString()); // Body
                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);

                //----------Master-------------------------------------       
                orderm sm = new orderm();
                sm.ormId = Guid.NewGuid().ToString();
               // sm.ordName = shippingInfo.client_name;
                //sm.Unvan = shippingInfo.client_sity;
                //sm.ordTelefon = shippingInfo.client_phone;
                //sm.otptarix = System.DateTime.Now;
                // sm.otptarix = System.Convert.ToDateTime("01/01/2015");
                sm.summ = cart.ComputeTotalValue();
                //sm.getdi = false;
                _orderm.InsertAsync(sm);
                //var sonmas = repository.SifMByID(cart.ComputeTotalValue());
                // var iid = sonmas.SifID;
                //-----------------------------------------------
                foreach (var line in cart.Lines)
                {
                    orderdet sif = new orderdet();
                    ////------Detal-------------------            
                    //sif.Prodname = line.Product.itemname;
                    //sif.Qountity = line.Quantity;
                    //sif.Unitselprice = line.Product.salesprice;
                    //sif.ormID = sm.ormID;
                    _orderd.InsertAsync(sif);
                    //-------------------------
                    var subtotal = line.Product.sell_unitprice * line.Quantity;
                    body.AppendFormat("{0} x {1} (Aralıq məbləğ: {2:c})\n", line.Quantity,
                    line.Product.prodname, subtotal);
                }
            }
        }        
    }
}
