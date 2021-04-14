using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBridge.ApplicationCode.Common_Implementation
{
    public class BaseController : Controller
    {
        
        protected virtual CustomPrincipal CurrentUser
        {
            get { return System.Web.HttpContext.Current.User as CustomPrincipal; }
        }

    }
}