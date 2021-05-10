using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnBazar.Data;
using OnBazar.Models;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using OnBazar.Services;
using System.Text;

namespace OnBazar.Models
{
    
    public class DummyData
    {
        private readonly IRepository<_firma> _firma = null;
          public DummyData(IRepository<_firma> firma//, IRepository<Navbar> nav, IRepository<NavbarRole> navrol
              )
          {
           // _firma = firma;
            //  _nav = nav;
            //  _navrol = navrol;
        }

        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager )
        {
            context.Database.EnsureCreated();
            string _user = "agakamran@yandex.ru";
            string[] _role = { "Administrator", "Operator", "User" };


            string password = "123456";
            if (await _roleManager.FindByNameAsync(_role[0]) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(_role[0]));
            }
            if (await _roleManager.FindByNameAsync(_role[1]) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(_role[1]));
            }
            if (await _roleManager.FindByNameAsync(_role[2]) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(_role[2]));
            }
            if (await _userManager.FindByEmailAsync(_user) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = _user,
                    Email = _user
                };
                // var result = await _userManager.CreateAsync(user, model.Password);
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, _role[0]);
                    await _userManager.AddToRoleAsync(user, _role[1]);
                    await _userManager.AddToRoleAsync(user, _role[2]);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var _result = await _userManager.ConfirmEmailAsync(user, code);
                }
              /*  if (_firma.GetAll().FirstOrDefault(f => f.firma_name == user.UserName && f.firma_email == user.Email) == null)
                {
                    var pp = new _firma();
                    pp.firma_Id = Guid.NewGuid().ToString();
                    pp.firma_name = user.UserName;
                    pp.firma_telefon = user.PhoneNumber;
                    pp.firma_unvan = "";
                    pp.firma_email = user.Email;
                    pp.userId = user.Id;
                    await _firma.InsertAsync(pp);

                }*/
            }
           
        }

        public string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new System.Security.Cryptography.SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hashBytes);
        }
        public string GetMacAddress()
        {
            string macAddresses = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }  

}
