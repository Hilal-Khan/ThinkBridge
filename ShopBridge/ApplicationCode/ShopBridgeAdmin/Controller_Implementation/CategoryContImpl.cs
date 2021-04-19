using System;
using System.Collections.Generic;
using System.Linq;
using ShopBridge.DAL;
using ShopBridge.Models;
using System.Web.Mvc;
using ShopBridge.HelperUtility;
using ShopBridge.ApplicationCode.Common_Implementation;
using ShopBridge.Areas.Admin.Models;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction;
using ShopBridge.Areas.Admin.Controllers;

namespace ShopBridge.ApplicationCode.VegShopAdmin.Controller_Implementation
{
    public class CategoryContImpl : BaseController, ICategoryContImpl
    {
        CategoryRepository CatRepo = new CategoryRepository();

        protected virtual ActionResult Index()
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int compId = cp.CompanyID;
                    return View(CatRepo.getCategory(compId));
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                return HandleException.CustomException("Index", "Category");
            }
        }

        protected virtual ActionResult Create()
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int compId = cp.CompanyID;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                return HandleException.CustomException("Create", "Category");
            }   
        }

        protected virtual ActionResult CreatePost(Category oNewCategory, FormCollection oNewForm)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
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
                else
                {
                    return RedirectToAction("Index", "Login");
                }
                
            }
            catch (Exception ex)
            {
                return HandleException.CustomException("CreatePost", "Category");
            }
        }

        protected virtual ActionResult Edit(int id)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int compId = cp.CompanyID;
                    int userID = cp.CurrentUserId;
                    Category oCategory = CatRepo.getCategory(id, compId).FirstOrDefault();
                    if (oCategory != null)
                    {
                        return View(oCategory);
                    }
                    else
                    {
                        return HandleException.CustomException("Edit", "Category");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                return HandleException.CustomException("Edit", "Category");
            }
        }

        protected virtual ActionResult EditPost(Category oNewCategory, FormCollection oNewForm)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int compId = cp.CompanyID;
                    int userID = cp.CurrentUserId;
                    oNewCategory.EditedOn = DateTime.Now;
                    oNewCategory.EditedBy = userID;
                    CatRepo.updateCategory(oNewCategory);
                    this.ShowMessage(ConstantEnums.MessageType.Success, "Updated Successfully");
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
                

            }
            catch (Exception ex)
            {
                return HandleException.CustomException("EditPost", "Category");
            }
        }

        protected virtual ActionResult Delete(int id)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
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
                        return HandleException.CustomException("Delete", "Category");
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
                
            }
            catch (Exception ex)
            {
                return HandleException.CustomException("Delete", "Category");
            }
        }
        
        ActionResult ICategoryContImpl.Index() { return Index(); }
        ActionResult ICategoryContImpl.Create() { return Create(); }
        ActionResult ICategoryContImpl.CreatePost(Category Category, FormCollection oNewForm) { return CreatePost(Category, oNewForm); }
        ActionResult ICategoryContImpl.Edit(int id) { return Edit(id); }
        ActionResult ICategoryContImpl.EditPost(Category Category, FormCollection oNewForm) { return EditPost(Category, oNewForm); }
        ActionResult ICategoryContImpl.Delete(int id) { return Delete(id); }
    }
}