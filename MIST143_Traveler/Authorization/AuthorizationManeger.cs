using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MIST143_Traveler.Models;

namespace MIST143_Traveler.Authorization
{
    public class AuthorizationManeger : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString(CDictionary.SK_Login) ==null)
                context.Result=new RedirectToRouteResult(new {controller= "CustomerCenter", action= "newLoginpag" });
        }
    }
}
