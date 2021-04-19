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
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int compId = cp.CompanyID;
                    int userID = cp.CurrentUserId;
                    Product oProduct = ProductRepo.getProduct(id, compId).FirstOrDefault();
                    if (oProduct != null)
                    {
                        
                    }
                    else
                    {
                        this.ShowMessage(ConstantEnums.MessageType.Error, "Sorry we are unable to retrieve Product details, Please contact Service provider for the same.");
                        return View("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ConstantEnums.MessageType.Error, "Sorry we are unable to retrieve Product details, Please contact Service provider for the same.");
                return View("Index");
            }
        }
    }
}