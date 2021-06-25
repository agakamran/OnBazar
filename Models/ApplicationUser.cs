using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using OnBazar.Services;

namespace OnBazar.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? providerId { get; set; }
        public long? providername { get; set; }
        public string photoUrl { get; set; }          
        public string IP { get; set; }
        public decimal percent { get; set; } = 0;
        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
        // public string MacAdd { get; set; }
        // public bool Hal { get; set; }
        // public decimal Mebleg { get; set; }
        //----------------------------------
        //public string fio { get; set; }       
        //public string vesige { get; set; }
        //public string telefon { get; set; }
        //public string MachineName { get; set; }
        //public string komuser { get; set; }
    }
}
