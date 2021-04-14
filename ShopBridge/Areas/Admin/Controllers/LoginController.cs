using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using System.Web.Security;
using ShopBridge.ApplicationCode.Common_Implementation;
using ShopBridge.ApplicationCode.Common_Abstraction;
using ShopBridge.DAL;
using ShopBridge.Models;
using ShopBridge.HelperUtility;
using ShopBridge.Areas.Admin.Models;
using static ShopBridge.Models.ConstantEnums;

namespace ShopBridge.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {

        SystemUserRepository SystemUserRepo = new SystemUserRepository();

        public ActionResult Index()
        {
            try
            {
                if (UserAuthorization.IsLoggedIn())
                {
                    return RedirectToAction("Index", "DashBoard", new { area = "Admin" });
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                return HandleException.CustomException(ex);
            }
        }

        [HttpPost]
        public ActionResult Index(SystemUser oSystemUser, FormCollection pFormCollection)
        {
            try
            {
                if (oSystemUser.LoginID == null || oSystemUser.Password == null || oSystemUser.Password == string.Empty || oSystemUser.LoginID == string.Empty)
                {
                    TempData["LoginError"] = "Invalid LoginId or password";
                    return View("Index");
                }
                var oUser = SystemUserRepo.getSystemUser(oSystemUser.LoginID, oSystemUser.Password).FirstOrDefault();
                if (oUser != null)
                {
                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.CurrentUserId = oUser.UserId;
                    serializeModel.LoginId = oUser.LoginID;
                    serializeModel.CurrentUserName = oUser.Name;
                    serializeModel.CompanyID = oUser.CompanyID.Value;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string userData = serializer.Serialize(serializeModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                   (1, oUser.UserId.ToString(), DateTime.Now, DateTime.Now.AddHours(8), false, userData);
                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    faCookie.Expires = DateTime.Now.AddHours(8);
                    Response.Cookies.Add(faCookie);
                    System.Web.HttpContext.Current.Response.Cookies.Add(faCookie);
                    System.Web.HttpContext.Current.Request.Cookies.Add(faCookie);
                    return RedirectToAction("Index", "DashBoard", new { area = "Admin" });
                }
                else
                {
                    TempData["LoginError"] = "Invalid LoginId or password";
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                return HandleException.CustomException(ex);
            }
        }

        [CustomAdminAuthentication]
        [Authorize]
        public ActionResult Logoff()
        {
            UserAuthorization.RemoveActivities();
            FormsAuthentication.SignOut();
            TempData["LoginError"] = "Successfully logout";
            return View("Index");
        }

    }
}