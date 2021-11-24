using GestionContact.DAL;
using GestionContactASP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactASP.Helpers
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            LoginSuccessDto user = SessionHelper.Get<LoginSuccessDto>(context.HttpContext.Session);
            if (user is null)
            {
                context.Result = new RedirectToActionResult("Login", "User", null);
            }
        }
    }
}