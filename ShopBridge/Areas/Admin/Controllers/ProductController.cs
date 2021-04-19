using ShopBridge.DAL;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopBridge.HelperUtility;
using System.Configuration;
using ShopBridge.ApplicationCode.Common_Implementation;
using ShopBridge.Areas.Admin.Models;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction;
using VegShopUI.ApplicationCode.VegShopAdmin.Controller_Implementation;

namespace ShopBridge.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository ProductRepo = new ProductRepository();
        CategoryRepository CatRepo = new CategoryRepository();

        private IProductContImpl oProductContImpl = new ProductContImpl();

        public ActionResult Index()
        {
            return oProductContImpl.Index();
        }

        public ActionResult Create()
        {
            return oProductContImpl.Create();

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Product oNewProduct, HttpPostedFileBase Imagename, FormCollection oNewForm)
        {
            return oProductContImpl.CreatePost(oNewProduct, Imagename, oNewForm);
        }

        public ActionResult Edit(int id)
        {
            return oProductContImpl.Edit(id);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Product oNewProduct, HttpPostedFileBase Imagename, FormCollection oNewForm)
        {
            return oProductContImpl.EditPost(oNewProduct, Imagename, oNewForm);
        }

        public ActionResult Delete(int id)
        {
            return oProductContImpl.Delete(id);
        }
    }
}