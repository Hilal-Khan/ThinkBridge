using System.Web.Mvc;
using ShopBridge.Models;

namespace ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction
{
    public interface ICategoryContImpl
    {
        ActionResult Index();
        ActionResult Create();
        ActionResult CreatePost(Category Category, FormCollection oNewForm);
        ActionResult Edit(int id);
        ActionResult EditPost(Category Category, FormCollection oNewForm);
        ActionResult Delete(int id);
    }
}
