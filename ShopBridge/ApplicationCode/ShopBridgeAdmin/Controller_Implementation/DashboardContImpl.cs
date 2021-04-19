using System.Web.Mvc;
using ShopBridge.ApplicationCode.Common_Implementation;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction;
using System;
using ShopBridge.HelperUtility;
using ShopBridge.Models;
using ShopBridge.Areas.Admin.Controllers;
using ShopBridge.Areas.Admin.Models;

namespace ShopBridge.ApplicationCode.VegShopAdmin.Controller_Implementation
{
    public class DashboardContImpl : BaseController, IDashboardContImpl
    {
        protected virtual ActionResult Index()
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int companyid = cp.CompanyID;
                    int userID = cp.CurrentUserId;
                    return View("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                return HandleException.CustomException("Index", "Dashboard");
            }
        }

        ActionResult IDashboardContImpl.Index() { return Index(); }

    }
}