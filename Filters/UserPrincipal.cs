using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace IndigoAdmin.Filters
{
    public class UserPrincipal : IUserPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }

        public UserPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public long UserId { get; set; }
        public long UserRoleId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string AuthToken { get; set; }
    }
    public interface IUserPrincipal : IPrincipal
    {
        long UserId { get; set; }
        string UserFirstName { get; set; }
        string UserLastName { get; set; }
        string AuthToken { get; set; }
    }
}