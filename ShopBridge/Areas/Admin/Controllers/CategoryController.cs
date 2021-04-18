using ShopBridge.DAL;
using ShopBridge.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using ShopBridge.HelperUtility;
using ShopBridge.ApplicationCode.Common_Implementation;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Implementation;

namespace ShopBridge.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository CategoryRepo = new CategoryRepository();
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
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int compId = cp.CompanyID;
                    int userID = cp.CurrentUserId;
                    Category oCategory = CategoryRepo.getCategory(id, compId).FirstOrDefault();
                    if (oCategory != null)
                    {
                        return oCategoryContImpl.Edit(oCategory);
                    }
                    else
                    {
                        this.ShowMessage(ConstantEnums.MessageType.Error, "Sorry we are unable to retrieve Category details, Please contact Service provider for the same.");
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
                this.ShowMessage(ConstantEnums.MessageType.Error, "Sorry we are unable to retrieve Category details, Please contact Service provider for the same.");
                return View("Index");
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Category Category, FormCollection pFormCollection)
        {
            return oCategoryContImpl.EditPost(Category, pFormCollection);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int compId = cp.CompanyID;
                    int userID = cp.CurrentUserId;
                    Category oCategory = CategoryRepo.getCategory(id, compId).FirstOrDefault();
                    if (oCategory != null)
                    {
                        return oCategoryContImpl.Delete(oCategory);
                    }
                    else
                    {
                        this.ShowMessage(ConstantEnums.MessageType.Error, "Sorry we are unable to retrieve Category details, Please contact Service provider for the same.");
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
                this.ShowMessage(ConstantEnums.MessageType.Error, "Sorry we are unable to retrieve Category details, Please contact Service provider for the same.");
                return View("Index");
            }
        }
    }
}
