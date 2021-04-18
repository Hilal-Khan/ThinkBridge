using System.Web.Mvc;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Implementation;

namespace ShopBridge.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        private IDashboardContImpl oDashboardContImpl = new DashboardContImpl();
        
        public ActionResult Index()
        {
            return View();
        }

    }
}