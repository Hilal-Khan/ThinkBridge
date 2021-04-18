using System.Web.Mvc;
using System.Web;
using ShopBridge.Models;

namespace ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction
{
    public interface ICategoryContImpl
    {
        ActionResult Index();
        ActionResult Create();
        ActionResult CreatePost(Category Category, FormCollection oNewForm);
        ActionResult Edit(Category Category);
        ActionResult EditPost(Category Category, FormCollection oNewForm);
        ActionResult Delete(Category Category);
    }
}
