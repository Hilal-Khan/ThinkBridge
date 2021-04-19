using System.Web.Mvc;
using System.Web;
using ShopBridge.Models;

namespace ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction
{
    public interface IProductContImpl
    {
        ActionResult Index();
        ActionResult Create();
        ActionResult CreatePost(Product oProduct, HttpPostedFileBase Imagename, FormCollection oNewForm);
        ActionResult Edit(int id);
        ActionResult EditPost(Product oProduct, HttpPostedFileBase Imagename, FormCollection oNewForm);
        ActionResult Delete(int id);
    }
}