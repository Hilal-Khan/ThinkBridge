using ShopBridge.HelperUtility;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ShopBridge.DAL;
using System.Configuration;
using ShopBridge.ApplicationCode.Common_Implementation;

namespace ShopBridge.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        public ActionResult Index()
        {
            CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
            int companyid = cp.CompanyID;
            int userID = cp.CurrentUserId;
            
            return View();
        }

    }
}