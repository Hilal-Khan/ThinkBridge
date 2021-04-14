using ShopBridge.ApplicationCode.Common_Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.ApplicationCode.Common_Implementation
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }
        public int CurrentUserId { get; set; }
        public string LoginID { get; set; }
        public string CurrentUserName { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public CustomPrincipal(string pUserId)
        {
            this.Identity = new GenericIdentity(pUserId);
        }

    }

    public class CustomPrincipalSerializeModel
    {
        public int CurrentUserId { get; set; }
        public string CurrentUserName { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string LoginId { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
    }
}