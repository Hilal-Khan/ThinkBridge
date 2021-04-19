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
        public static ViewResult CustomException(string action, string controller)
        {
            string responseMessage = "";

            responseMessage = "Sorry, we are currently unable to process " + controller + " operation, Please contact Service provider for the same.";
            
            return new ViewResult()
            {
                ViewName = "~/Areas/Admin/Views/Shared/ErrorMessage.cshtml",
                ViewData = new ViewDataDictionary() { { "message", responseMessage } }
            };
        }

    }
}