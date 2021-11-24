using GestionContact.CORE;
using GestionContact.CORE.servicesHTTP;
using GestionContact.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactApi.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthRequiredAttribute : TypeFilterAttribute
    {
        public AuthRequiredAttribute() : base(typeof(AuthRequiredFilter))
        {
        }

        private class AuthRequiredFilter : IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                ITokenService tokenService = (ITokenService)context.HttpContext.RequestServices.GetService(typeof(ITokenService));
               // IUserRepository<LoginSuccessDto> authService = (IUserRepository<LoginSuccessDto>)context.HttpContext.RequestServices.GetService(typeof(IUserRepository<LoginSuccessDto>));

                context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authorizations);
                string token = authorizations.SingleOrDefault(authorization => authorization.StartsWith("Bearer "));

                if (token is null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                LoginSuccessDto user = tokenService.ValidateToken(token);

                if (user is null)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                //if (authService.Check(user) is not null)
                //{
                //    context.Result = new UnauthorizedResult();
                //    return;
                //}
            }
        }
    }
}
