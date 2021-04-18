using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ShopBridge.ApplicationCode.Common_Implementation;
using ShopBridge.ApplicationCode.VegShopAdmin.Controller_Abstraction;
using ShopBridge.HelperUtility;
using ShopBridge.Models;
using ShopBridge.Areas.Admin.Models;
using ShopBridge.DAL;

namespace VegShopUI.ApplicationCode.VegShopAdmin.Controller_Implementation
{
    public class ProductContImpl : BaseController, IProductContImpl
    {

        ProductRepository ProductRepo = new ProductRepository();
        CategoryRepository CatRepo = new CategoryRepository();

        protected virtual ActionResult Index()
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int compId = cp.CompanyID;
                    int userID = cp.CurrentUserId;
                    return View(ProductRepo.getProduct(compId));
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

        protected virtual ActionResult Create()
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int compId = cp.CompanyID;
                    int userID = cp.CurrentUserId;
                    ViewBag.SequenceNo = ProductRepo.getProduct(compId).Select(o => o.SequenceNo).Max() + 1;
                    ViewBag.CategoryList = CatRepo.getCategory(compId);
                    return View();
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

        protected virtual ActionResult CreatePost(Product oNewProduct, HttpPostedFileBase Imagename, FormCollection pFormCollection)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int compId = cp.CompanyID;
                    int userID = cp.CurrentUserId;
                    if (Imagename != null && Imagename.ContentLength > 0)
                    {
                        oNewProduct.Image = new FileUploader().uploadFile(Imagename, oNewProduct.ProductName, ConstantEnums.FileUploaderPath.Product);
                    }
                    oNewProduct.CreatedOn = DateTime.Now;
                    oNewProduct.IsActive = true;
                    oNewProduct.IsDeleted = false;
                    oNewProduct.CompanyID = compId;
                    oNewProduct.CreatedBy = userID;
                    ProductRepo.addProduct(oNewProduct);
                    this.ShowMessage(ConstantEnums.MessageType.Success, "Product Details Saved Successfully!");
                    return RedirectToAction("Index");
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

        protected virtual ActionResult Edit(Product oProduct)
        {
            try
            {
                if (oProduct != null)
                {
                    CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                    if (cp != null)
                    {
                        int compId = cp.CompanyID;
                        ViewBag.CategoryList = CatRepo.getCategory(compId);
                        return View(oProduct);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    this.ShowMessage(ConstantEnums.MessageType.Error, "Sorry we are unable to retrieve Product details, Please contact Service provider for the same.");
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage(ConstantEnums.MessageType.Error, "Sorry we are unable to retrieve Product details, Please contact Service provider for the same.");
                return View("Index");
            }
        }

        protected virtual ActionResult EditPost(Product oNewProduct, HttpPostedFileBase Imagename, FormCollection pFormCollection)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int compId = cp.CompanyID;
                    int userID = cp.CurrentUserId;
                    if (Imagename != null && Imagename.ContentLength > 0)
                    {
                        string new_img_name = new FileUploader().uploadFile(Imagename, oNewProduct.ProductName, ConstantEnums.FileUploaderPath.Product);
                        new FileUploader().deleteFile(oNewProduct.Image, ConstantEnums.FileUploaderPath.Product);
                        oNewProduct.Image = new_img_name;
                    }

                    oNewProduct.EditedBy = userID;
                    oNewProduct.EditedOn = DateTime.Now;
                    ProductRepo.updateProduct(oNewProduct);
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
                this.ShowMessage(ConstantEnums.MessageType.Error, "Sorry we are unable to retrieve Product details, Please contact Service provider for the same.");
                return View("Index");
            }
        }

        protected virtual ActionResult Delete(Product oProduct)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                if (cp != null)
                {
                    int compId = cp.CompanyID;
                    int userID = cp.CurrentUserId;
                    if (oProduct != null)
                    {
                        oProduct.IsActive = false;
                        oProduct.IsDeleted = true;
                        oProduct.DeletedOn = DateTime.Now;
                        oProduct.DeletedBy = userID;
                        ProductRepo.updateProduct(oProduct);
                        this.ShowMessage(ConstantEnums.MessageType.Success, "Product Deleted Successfully");
                    }
                    else
                    {
                        this.ShowMessage(ConstantEnums.MessageType.Error, "Sorry we are unable to retrieve Product details, Please contact Service provider for the same.");
                        return View("Index");
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
                this.ShowMessage(ConstantEnums.MessageType.Error, "Sorry we are unable to retrieve Product details, Please contact Service provider for the same.");
                return View("Index");
            }
        }

        ActionResult IProductContImpl.Index() { return Index(); }
        ActionResult IProductContImpl.Create() { return Create(); }
        ActionResult IProductContImpl.CreatePost(Product oNewProduct, HttpPostedFileBase Imagename, FormCollection pFormCollection) { return CreatePost(oNewProduct, Imagename, pFormCollection); }
        ActionResult IProductContImpl.Edit(Product Product) { return Edit(Product); }
        ActionResult IProductContImpl.EditPost(Product oNewProduct, HttpPostedFileBase Imagename, FormCollection pFormCollection) { return EditPost(oNewProduct, Imagename, pFormCollection); }
        ActionResult IProductContImpl.Delete(Product Product) { return Delete(Product); }
        
    }
}