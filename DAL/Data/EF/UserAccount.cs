using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace IndigoAdmin.DAL.Data.EF
{
    public partial class UserAccount
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        public string UserUuid { get; set; }
        public int? UserRoleId { get; set; }
        public int? AssignedOrderTakerId { get; set; }
        public int? AssignedDeliveryBoyId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmailAddress { get; set; }
        public string UserPassword { get; set; }
        public string UserProfilePicture { get; set; }
        public string UserPhone { get; set; }
        public string UserSore { get; set; }
        public bool? IsPasswordReset { get; set; }
        public bool? IsVerified { get; set; }
        public bool? ActiveStatus { get; set; }
        public string Address { get; set; }
        public int AreaId { get; set; }
        public string BillingAddress { get; set; }
        public string Logitude { get; set; }
        public string Latitude { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? CreatedBy { get; set; }
        public long ReferralId { get; set; }
        public int? ModifiedBy { get; set; }
        [NotMapped]
        public string AuthToken { get; set; }
        [NotMapped]
        public int Orders { get; set; }

        [NotMapped]
        public LicenseInformation LicenseInformation { get; set; }

        [NotMapped]
        public bool IsChequeAllowed { get; set; }
        [NotMapped]
        public bool IsCreditAllowed { get; set; }
        [NotMapped]
        public string AssignedUserName { get; set; }
    }
}
