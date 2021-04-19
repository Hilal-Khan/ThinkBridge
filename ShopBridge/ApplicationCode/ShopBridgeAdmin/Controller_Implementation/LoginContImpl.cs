using System.Web.Mvc;
using ShopBridge.ApplicationCode.Common_Implementation;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction;
using System;
using System.Linq;
using ShopBridge.HelperUtility;
using ShopBridge.Models;
using ShopBridge.Areas.Admin.Controllers;
using ShopBridge.Areas.Admin.Models;
using System.Web.Security;
using ShopBridge.DAL;
using System.Web.Script.Serialization;
using System.Web;

namespace ShopBridge.ApplicationCode.VegShopAdmin.Controller_Implementation
{
    public class LoginContImpl : BaseController, ILoginContImpl
    {

        SystemUserRepository SystemUserRepo = new SystemUserRepository();

        protected virtual ActionResult Index()
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
                return HandleException.CustomException("Index", "Login");
            }
        }
        
        public virtual ActionResult IndexPost(SystemUser oSystemUser, FormCollection oNewForm)
        {
            return null;
            //try
            //{
            //    if (oSystemUser.LoginID == null || oSystemUser.Password == null || oSystemUser.Password == string.Empty || oSystemUser.LoginID == string.Empty)
            //    {
            //        TempData["LoginError"] = "Invalid LoginId or password";
            //        return View("Index");
            //    }
            //    var oUser = SystemUserRepo.getSystemUser(oSystemUser.LoginID, oSystemUser.Password).FirstOrDefault();
            //    if (oUser != null)
            //    {
            //        CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            //        serializeModel.CurrentUserId = oUser.UserId;
            //        serializeModel.LoginId = oUser.LoginID;
            //        serializeModel.CurrentUserName = oUser.Name;
            //        serializeModel.CompanyID = oUser.CompanyID.Value;
            //        JavaScriptSerializer serializer = new JavaScriptSerializer();
            //        string userData = serializer.Serialize(serializeModel);
            //        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
            //       (1, oUser.UserId.ToString(), DateTime.Now, DateTime.Now.AddHours(8), false, userData);

            //        string encTicket = FormsAuthentication.Encrypt(authTicket);
            //        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            //        faCookie.Expires = DateTime.Now.AddHours(8);
            //        Response.Cookies.Add(faCookie);
            //        System.Web.HttpContext.Current.Response.Cookies.Add(faCookie);
            //        System.Web.HttpContext.Current.Request.Cookies.Add(faCookie);
            //        return RedirectToAction("Index", "DashBoard", new { area = "Admin" });
            //    }
            //    else
            //    {
            //        TempData["LoginError"] = "Invalid LoginId or password";
            //    }
            //    return View("Index");
            //}
            //catch (Exception ex)
            //{
            //    return HandleException.CustomException("IndexPost", "Login");
            //}
        }

        [CustomAdminAuthentication]
        [Authorize]
        protected virtual ActionResult Logoff()
        {
            UserAuthorization.RemoveActivities();
            FormsAuthentication.SignOut();
            TempData["LoginError"] = "Successfully logout";
            return View("Index");
        }


        ActionResult ILoginContImpl.Index() { return Index(); }
        ActionResult ILoginContImpl.IndexPost(SystemUser oSystemUser, FormCollection oNewForm) { return IndexPost(oSystemUser, oNewForm); }
        ActionResult ILoginContImpl.Logoff() { return Logoff(); }

    }
}