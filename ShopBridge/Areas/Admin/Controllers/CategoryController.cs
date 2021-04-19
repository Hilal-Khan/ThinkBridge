using ShopBridge.Models;
using System.Web.Mvc;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Implementation;

namespace ShopBridge.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
           
        private ICategoryContImpl oCategoryContImpl = new CategoryContImpl();

        public ActionResult Index()
        {
            return oCategoryContImpl.Index();
        }

        public ActionResult Create()
        {
            return oCategoryContImpl.Create();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Category Category, FormCollection pFormCollection)
        {
            return oCategoryContImpl.CreatePost(Category, pFormCollection);
        }

        public ActionResult Edit(int id)
        {
            return oCategoryContImpl.Edit(id);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Category Category, FormCollection pFormCollection)
        {
            return oCategoryContImpl.EditPost(Category, pFormCollection);
        }

        public ActionResult Delete(int id)
        {
            return oCategoryContImpl.Delete(id);
        }

    }
}
