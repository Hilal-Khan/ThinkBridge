using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShopBridge.Areas.Admin.Models
{
    public class HandleException
    {
        public static ViewResult CustomException(Exception pException)
        {
            string responseMessage = "";
            if (pException.Message.ToLower().Contains("unique key"))
                responseMessage = "You can not insert duplicate value";
            else if (pException.Message.ToLower().Contains("duplicate key"))
                responseMessage = "You can not insert duplicate value";
            else
            {
                string str = pException.StackTrace;
                responseMessage = "Error : " + pException.Message.ToString() + "->StackTrace:" + str;// +" -> " + str.Substring(pException.StackTrace.LastIndexOf('\\'));
            }
            return new ViewResult()
            {
                ViewName = "~/Areas/Admin/Views/Shared/_204ApplicationWentWrong.cshtml",
                ViewData = new ViewDataDictionary() { { "message", responseMessage } }
            };
        }

        public static ViewResult CustomException(string pException)
        {
            return new ViewResult()
            {
                ViewName = "~/Areas/Admin/Views/Shared/Error.cshtml",
                ViewData = new ViewDataDictionary() { { "message", pException } }
            };
        }

        public static ViewResult UnauthorizedAccess()
        {
            //var responseMessage = "You are not authorized to access this resource.";

            return new ViewResult()
            {
                ViewName = "~/Areas/Admin/Views/Shared/_401UnAuthrisedAccess.cshtml"
                //ViewData = new ViewDataDictionary() { { "message", responseMessage } }
            };
        }

        public static ViewResult PageNotFound()
        {
            return new ViewResult()
            {
                ViewName = "~/Areas/Admin/Views/Shared/_404PageNotFound.cshtml"
            };
        }

        public static ViewResult SomethingWentWrong()
        {
            return new ViewResult()
            {
                ViewName = "~/Areas/Admin/Views/Shared/_204SomethingWentWrong.cshtml"
            };
        }

        public static ViewResult IssueOnLive()
        {
            return new ViewResult()
            {
                ViewName = "~/Areas/Admin/Views/Shared/_Issue_On_Live.cshtml"
            };
        }

        public static ViewResult FileNotFound()
        {
            return new ViewResult()
            {
                ViewName = "~/Areas/Admin/Views/Shared/_404FileNotFound.cshtml"
            };
        }
    }
}