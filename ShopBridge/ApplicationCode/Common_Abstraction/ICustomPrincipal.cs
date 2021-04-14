using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.ApplicationCode.Common_Abstraction
{
    public interface ICustomPrincipal : IPrincipal
    {
        int CurrentUserId { get; set; }
        string LoginID { get; set; }
        string CurrentUserName { get; set; }
        string ProfilePhotoUrl { get; set; }
        int CompanyID { get; set; }
        string CompanyName { get; set; }
        string CompanyLogo { get; set; }

    }
}