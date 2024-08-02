using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Models
{
    public class SignupModal
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmailAddress { get; set; }
        public string UserPassword { get; set; }
        public string UserProfilePicture { get; set; }
        public string UserPhone { get; set; }
        public bool? IsPasswordReset { get; set; }
        public bool? ActiveStatus { get; set; }
        public string Address { get; set; }
        public string BillingAddress { get; set; }
        public string Logitude { get; set; }
        public string Latitude { get; set; }
        public string UserSore { get; set; }

        public long ReferralId { get; set; }

        public DateTime? LicenseExpiry { get; set; }

        public IFormFile LicenseFile { get; set; }
    }
}
