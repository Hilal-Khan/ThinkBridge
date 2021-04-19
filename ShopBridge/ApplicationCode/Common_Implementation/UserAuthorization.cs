using ShopBridge.ApplicationCode.Common_Abstraction;
using ShopBridge.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopBridge.Models;

namespace ShopBridge.ApplicationCode.Common_Implementation
{
    public class UserAuthorization : IAuthorization
    {
        
        public static bool IsLoggedIn()
        {
            CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
            if (cp != null && cp.CompanyID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public static void RemoveActivities()
        {
            if (IsLoggedIn())
            {
                string CurrentUserId = (HttpContext.Current.User as CustomPrincipal).CurrentUserId.ToString();
                if (HttpContext.Current.Cache[CurrentUserId] != null && CurrentUserId != string.Empty)
                    HttpContext.Current.Cache.Remove(CurrentUserId);
            }
        }
        
        void IAuthorization.RemoveActivities() { RemoveActivities(); }
    }
}