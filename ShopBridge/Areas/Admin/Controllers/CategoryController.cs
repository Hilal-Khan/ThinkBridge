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

namespace ShopBridge.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository CatRepo = new CategoryRepository();
        
        public ActionResult Index()
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                int compId = cp.CompanyID;
                return View(CatRepo.getCategory(compId));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ConstantEnums.MessageType.Error, "Error in retriving details of Category " + ex.Message);
                return View();
            }
        }

        public ActionResult Create()
        {
            CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
            int compId = cp.CompanyID;
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Category oNewCategory, FormCollection oNewForm)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                int compId = cp.CompanyID;
                int userID = cp.CurrentUserId;
                oNewCategory.CreatedOn = DateTime.Now;
                oNewCategory.IsActive = true;
                oNewCategory.IsDeleted = false;
                oNewCategory.CompanyID = compId;
                oNewCategory.CreatedBy = userID;
                CatRepo.addCategory(oNewCategory);
                this.ShowMessage(ConstantEnums.MessageType.Success, "Category Details Saved Successfully!");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ShowMessage(ConstantEnums.MessageType.Error, "Error in creating Category " + ex.Message);
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                int compId = cp.CompanyID;
                int userID = cp.CurrentUserId;
                if (id.HasValue)
                {
                    List<Category> lstCategory = CatRepo.getCategory(id.Value, compId);
                    if (lstCategory != null && lstCategory.Count > 0)
                    {
                        return View(lstCategory[0]);
                    }
                    else
                    {
                        return HandleException.PageNotFound();
                    }
                }
                else
                {
                    return HandleException.PageNotFound();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ConstantEnums.MessageType.Error, "Error in retriving details of Category " + ex.Message);
                return View();
            }
        }
        
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Category oNewCategory)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                int compId = cp.CompanyID;
                int userID = cp.CurrentUserId;
                oNewCategory.EditedOn = DateTime.Now;
                oNewCategory.EditedBy = userID;
                CatRepo.updateCategory(oNewCategory);   
                this.ShowMessage(ConstantEnums.MessageType.Success, "Updated Successfully");
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                this.ShowMessage(ConstantEnums.MessageType.Error, "Error in editing Category details " + ex.Message);
                return View();
            }
        }
        
        public ActionResult Delete(int id)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                int compId = cp.CompanyID;
                int userID = cp.CurrentUserId;
                Category oCategory = CatRepo.getCategory(id, compId).FirstOrDefault();
                if (oCategory != null)
                {
                    oCategory.IsActive = false;
                    oCategory.IsDeleted = true;
                    oCategory.DeletedOn = DateTime.Now;
                    oCategory.DeletedBy = userID;
                    CatRepo.updateCategory(oCategory);
                    this.ShowMessage(ConstantEnums.MessageType.Success, "Category Deleted Successfully");
                }
                else
                {
                    return HandleException.PageNotFound();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ShowMessage(ConstantEnums.MessageType.Error, "Error in deleting Category" + ex.Message);
                return View();
            }
        }
    }
}
