using ShopBridge.Models;
using System.Web.Mvc;

namespace ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction
{
    public interface ILoginContImpl
    {
        ActionResult Index();
        ActionResult IndexPost(SystemUser oSystemUser, FormCollection pFormCollection);
        ActionResult Logoff();
    }
}
