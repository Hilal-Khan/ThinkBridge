using ShopBridge.ApplicationCode.Common_Implementation;
using System.Web.Mvc;

namespace ShopBridge.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {

        protected virtual CustomPrincipal CurrentUser
        {
            get { return System.Web.HttpContext.Current.User as CustomPrincipal; }
        }

    }
}