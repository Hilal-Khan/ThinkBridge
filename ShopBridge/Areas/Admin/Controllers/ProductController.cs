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
    public class ProductController : Controller
    {
        ProductRepository ProductRepo = new ProductRepository();
        CategoryRepository CatRepo = new CategoryRepository();

        public ActionResult Index()
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                int compId = cp.CompanyID;
                int userID = cp.CurrentUserId;
                return View(ProductRepo.getProduct(compId));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ConstantEnums.MessageType.Error, "Error in retriving details of Product " + ex.Message);
                return View();
            }
        }

        public ActionResult Create()
        {
            CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
            int compId = cp.CompanyID;
            int userID = cp.CurrentUserId;
            ViewBag.SequenceNo = ProductRepo.getProduct(compId).Select(o => o.SequenceNo).Max() + 1;
            ViewBag.CategoryList = CatRepo.getCategory(compId);
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Product oNewProduct, HttpPostedFileBase Imagename, FormCollection oNewForm)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
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
            catch (Exception ex)
            {
                this.ShowMessage(ConstantEnums.MessageType.Error, "Error in creating Product " + ex.Message);
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
                    int compId = cp.CompanyID;
                    List<Product> lstProduct = ProductRepo.getProduct(id.Value, compId);
                    if (lstProduct != null && lstProduct.Count > 0)
                    {
                        ViewBag.CategoryList = CatRepo.getCategory(compId);
                        return View(lstProduct[0]);
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
                this.ShowMessage(ConstantEnums.MessageType.Error, "Error in retriving details of Product " + ex.Message);
                return View();
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Product oNewProduct, HttpPostedFileBase Imagename, FormCollection oNewForm)
        {
            try
            {
                CustomPrincipal cp = (System.Web.HttpContext.Current.User as CustomPrincipal);
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
            catch (Exception ex)
            {
                this.ShowMessage(ConstantEnums.MessageType.Error, "Error in editing Product details " + ex.Message);
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
                Product oProduct = ProductRepo.getProduct(id, compId).FirstOrDefault();
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
                    return HandleException.PageNotFound();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                this.ShowMessage(ConstantEnums.MessageType.Error, "Error in deleting Product " + ex.Message);
                return View();
            }
        }
    }
}