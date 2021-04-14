using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ShopBridge.ApplicationCode.Common_Implementation
{
    public class CustomAdminAuthentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie authCookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {

                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("area", "Admin");
                redirectTargetDictionary.Add("action", "Index");
                redirectTargetDictionary.Add("controller", "Login");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
            else
            {
                if (authCookie.Value == null || authCookie.Value == string.Empty)
                {
                    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                    redirectTargetDictionary.Add("area", "Admin");
                    redirectTargetDictionary.Add("action", "Index");
                    redirectTargetDictionary.Add("controller", "Login");
                    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}