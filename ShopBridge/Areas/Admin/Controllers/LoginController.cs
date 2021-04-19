using System.Web.Mvc;
using ShopBridge.ApplicationCode.Common_Implementation;
using ShopBridge.DAL;
using ShopBridge.Models;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Implementation;
using System.Web.Script.Serialization;
using System.Linq;
using System.Web.Security;
using System.Web;
using System;
using ShopBridge.Areas.Admin.Models;

namespace ShopBridge.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        
        SystemUserRepository SystemUserRepo = new SystemUserRepository();

        private ILoginContImpl oLoginContImpl = new LoginContImpl();

        public ActionResult Index()
        {
            return oLoginContImpl.Index();
        }

        [HttpPost]
        public ActionResult Index(SystemUser oSystemUser, FormCollection oNewForm)
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
                return HandleException.CustomException("IndexPost", "Login");
            }
        }

        [CustomAdminAuthentication]
        [Authorize]
        public ActionResult Logoff()
        {
            return oLoginContImpl.Logoff();
            //UserAuthorization.RemoveActivities();
            //FormsAuthentication.SignOut();
            //TempData["LoginError"] = "Successfully logout";
            //return View("Index");
        }

    }
}