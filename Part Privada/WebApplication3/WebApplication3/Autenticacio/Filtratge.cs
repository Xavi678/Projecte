using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Autenticacio
{
    public class Filtratge : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["login"] == null)
            {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}