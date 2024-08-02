using IndigoAdmin.DAL.Data.EF;
using IndigoAdmin.Helpers;
using IndigoAdmin.Models;
using IndigoAdmin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Controllers
{
    [Route("api/[controller]/[action]")]
   [ApiController]

    public class AuthenticationController : ControllerBase
    {
        IUserAccountService userAccountService;
        ILicenseInformationService licenseService;
        IUserTokenService userTokenService;
        private readonly IHostingEnvironment hostingEnvironment;

        IndigoDbContext db = new IndigoDbContext();

        public AuthenticationController(IUserAccountService userAccountService, IUserTokenService userTokenService, ILicenseInformationService licenseService, IHostingEnvironment environment)
        {
            this.userAccountService = userAccountService;
            this.userTokenService = userTokenService;
            this.licenseService = licenseService;
            hostingEnvironment = environment;
        }
        [AllowAnonymous]
        [HttpPost]
        public LoginResponse Login(string username, string password)
        {
            LoginResponse objResult = new LoginResponse();
            objResult.httpCode = 404;
            var temp_user = userAccountService.GetByEmailAndPassword(username, password);

            if (temp_user != null)
            {
                if (temp_user.UserRoleId == 3&&temp_user.IsVerified==false)
                {
                    objResult.httpCode = 400;
                }
                else
                {
                    UserToken objUserToken = new UserToken();
                    objUserToken.AuthToken = JwtAuthManager.GenerateToken(temp_user.UserEmailAddress);
                    objUserToken.UserId = temp_user.UserId;
                    objUserToken.IssueOn = DateTime.UtcNow;

                    userTokenService.Create(objUserToken);

                    temp_user.IsChequeAllowed = true;
                    temp_user.IsCreditAllowed = true;

                    temp_user.UserPassword = "";
                    temp_user.AuthToken = objUserToken.AuthToken;
                    objResult.httpCode = 200;
                    objResult.userData = temp_user;
                }
            }
            return objResult;
        }

        [HttpGet]
        public bool Logout()
        {
            //IEnumerable<string> temp_values = new IEnumerable<string>();
            var is_token_exist = Request.Headers.TryGetValue("Authorization", out var temp_values);
            if (is_token_exist)
            {
                var auth_token = temp_values.First();
                userTokenService.DeleteByAuthToken(auth_token);
            }

            return true;
        }

        [AllowAnonymous]
        [HttpPost]
        public int Signup([FromForm] SignupModal signupModal)
        {
            try
            {
                if (signupModal.ReferralId != 0)
                {
                    var referralAccount = userAccountService.GetById(signupModal.ReferralId);
                    if (referralAccount == null)
                    {
                        return 2;//Invalid Refferal Error
                    }
                }
                var newUser = new UserAccount()
                {
                    UserFirstName = signupModal.UserFirstName,
                    UserLastName = signupModal.UserLastName,
                    Address = signupModal.Address,
                    BillingAddress = signupModal.BillingAddress,
                    Latitude = signupModal.Latitude,
                    Logitude = signupModal.Logitude,
                    UserEmailAddress = signupModal.UserEmailAddress,
                    UserPhone = signupModal.UserPhone,
                    UserProfilePicture = signupModal.UserProfilePicture,
                    UserPassword = signupModal.UserPassword,
                    UserSore = signupModal.UserSore,
                    UserRoleId = 3,
                    IsVerified = false,
                    ReferralId=signupModal.ReferralId
                };
                var UserId = userAccountService.Create(newUser);
                var newLicense = new LicenseInformation()
                {
                    CreatedAt = DateTime.Now,
                    LicenseExpiry = signupModal.LicenseExpiry,
                    UserId = (int)UserId
                };
                var license = licenseService.Create(newLicense);
                if (signupModal.LicenseFile != null)
                {
                    var uniqueFileName = GetUniqueFileName(signupModal.LicenseFile.FileName);
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "license", license + "");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    signupModal.LicenseFile.CopyTo(new FileStream(filePath, FileMode.Create));
                    var oldData = licenseService.GetById(license);
                    oldData.LicenseFile = uniqueFileName;
                    licenseService.Update(oldData);
                }
            }
            catch(Exception e)
            {
                return 1;//General Error
            }
            return 0;//Success
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }


        public List<Area> GetAllAreas()
        {
            var list = db.Areas.OrderBy(x=>x.Title).ToList();

            if (list != null)
            {
                return list;
            }
            return new List<Area>();
        }
    }
}
