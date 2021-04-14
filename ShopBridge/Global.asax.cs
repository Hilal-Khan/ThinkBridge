using ShopBridge.ApplicationCode.Common_Abstraction;
using ShopBridge.ApplicationCode.Common_Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ShopBridge
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                try
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    CustomPrincipalSerializeModel serializeModel =
                    serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);
                    if (serializeModel != null)
                    {
                        CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                        newUser.CurrentUserId = serializeModel.CurrentUserId;
                        newUser.LoginID = serializeModel.LoginId;
                        newUser.ProfilePhotoUrl = serializeModel.ProfilePhotoUrl;
                        newUser.CompanyID = serializeModel.CompanyID;
                        newUser.CompanyName = serializeModel.CompanyName;
                        newUser.CompanyLogo = serializeModel.CompanyLogo;
                        newUser.CurrentUserName = serializeModel.CurrentUserName;
                        //newUser.PlatformId = serializeModel.PlatformId;
                        HttpContext.Current.User = newUser;
                    }
                }
                catch (Exception ex)
                {
                    UserAuthorization.RemoveActivities();
                    FormsAuthentication.SignOut();

                    var dataKey = "__ControllerTempData";

                    if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session[dataKey] != null)
                    {
                        var dataDict = HttpContext.Current.Session[dataKey] as IDictionary<string, object>;
                        if (dataDict == null)
                        {
                            /* what do you want to do? add a new IDict<> and put in session? */
                        }
                        else
                        {
                            dataDict["LoginError"] = "Successfully logout from System : " + ex.Message;
                            HttpContext.Current.Session[dataKey] = dataDict;
                        }
                    }
                    var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                    Response.Redirect(urlHelper.Action("Index", "Login", new { Area = "Admin" }));
                }

            }
        }


    }
}
