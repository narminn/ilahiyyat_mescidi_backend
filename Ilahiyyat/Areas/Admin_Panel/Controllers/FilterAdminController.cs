using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ilahiyyat.Areas.Admin_Panel.Controllers
{
    public class FilterAdmin : AuthorizeAttribute, IAuthorizationFilter
    {
        // GET: Admin_Panel/FilterAdmin
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }

            // Check for authorization
            if (HttpContext.Current.Session["AdminEmail"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin_Panel/Home/Login");
            }
        }
    }
}