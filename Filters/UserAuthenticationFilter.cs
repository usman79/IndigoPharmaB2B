using System;
using IndigoAdmin.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using IndigoAdmin.Helpers;

namespace IndigoAdmin.Filters
{
    public class UserAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authenticated = false;
            var userTokenService = context.HttpContext.RequestServices.GetRequiredService<IUserTokenService>();
            var userAccountService = context.HttpContext.RequestServices.GetRequiredService<IUserAccountService>();
            bool IsTokenValidated = false;
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out var listVals);

            if (listVals.Count!=0)
            {
                var auth_token = listVals[0];

                //var principal = JwtAuthManager.GetPrincipal(auth_token);

                var temp_user_token = userTokenService.GetByAuthToken(auth_token);
                if (temp_user_token != null)
                {
                    IsTokenValidated = true;
                }

                if (IsTokenValidated)
                {
                    var temp_user = userAccountService.GetById((long)temp_user_token.UserId);

                    UserPrincipal principal = new UserPrincipal(temp_user_token.UserId + "@indigo.com");
                    principal.UserId = (long)temp_user_token.UserId;
                    principal.AuthToken = auth_token;
                    principal.UserFirstName = temp_user.UserFirstName;
                    principal.UserLastName = temp_user.UserLastName;
                    principal.UserRoleId = (long)temp_user.UserRoleId;
                    if (temp_user != null)
                    {
                        authenticated = true;
                    }
                    UserPrncipleManager.Principle = principal;
                }
            }
            //check access 
            if (authenticated)
            {
                //all good, add optional code if you want. Or don't
            }
            else
            { 
                context.Result = new UnauthorizedResult();
            }
        }
    }
    //{
    //    //public bool AllowMultiple { get { return false; } }

    //    //public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
    //    //{
    //    //    var requestScope = context.Request.GetDependencyScope();
    //    //    var userTokenService = requestScope.GetService(typeof(IUserTokenService)) as IUserTokenService;
    //    //    var userAccountService = requestScope.GetService(typeof(IUserAccountService)) as IUserAccountService;

    //    //    bool IsTokenValidated = false;
    //    //    IEnumerable<string> listVals = null;
    //    //    context.Request.Headers.TryGetValues("Authorization", out listVals);

    //    //    if (listVals != null && listVals.Count() > 0)
    //    //    {
    //    //        var auth_token = listVals.FirstOrDefault();

    //    //        //var principal = JwtAuthManager.GetPrincipal(auth_token);

    //    //        var temp_user_token = userTokenService.GetByAuthToken(auth_token);
    //    //        if (temp_user_token != null)
    //    //        {
    //    //            IsTokenValidated = true;
    //    //        }

    //    //        if (IsTokenValidated)
    //    //        {
    //    //            var temp_user = userAccountService.GetById((long)temp_user_token.UserId);

    //    //            UserPrincipal principal = new UserPrincipal(temp_user_token.UserId + "@indigo.com");
    //    //            principal.UserId = (long)temp_user_token.UserId;
    //    //            principal.AuthToken = auth_token;
    //    //            principal.UserFirstName = temp_user.UserFirstName;
    //    //            principal.UserLastName = temp_user.UserLastName;
    //    //            principal.UserRoleId = (long)temp_user.UserRoleId;
    //    //            context.Principal = principal;
    //    //        }
    //    //    }

    //    //    return Task.FromResult(0);
    //    //}

    //    //public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
    //    //{
    //    //    return Task.FromResult(0);
    //    //}
    //}
}