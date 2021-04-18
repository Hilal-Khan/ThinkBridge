using ShopBridge.ApplicationCode.Common_Implementation;
using ShopBridge.DAL;
using ShopBridge.HelperUtility;
using ShopBridge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Data.Entity.Validation;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ShopBridge.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ShopBridgeAPIController : ApiController
    {
        private ShopBridgeEntities db = new ShopBridgeEntities();

        CategoryRepository CatRepo = new CategoryRepository();
        SystemUserRepository SystemUserRepo = new SystemUserRepository();
        ProductRepository ProductRepo = new ProductRepository();

        public async Task<List<CategoryViewModel>> GetAllCategories(int companyId)
        {

            var list = await db.Categories.Where(w => w.CompanyID == companyId && w.IsActive == true).ToListAsync();
            List<CategoryViewModel> lstCategoryViewModel = new List<CategoryViewModel>();
            if (list != null && list.Count() > 0)
            {
                foreach (Category item in list)
                {
                    CategoryViewModel oCategoryViewModel = new CategoryViewModel();
                    oCategoryViewModel.CategoryID = item.CategoryID;
                    oCategoryViewModel.CategoryName = item.CategoryName;
                    oCategoryViewModel.CategoryDescription = item.CategoryDescription;
                    lstCategoryViewModel.Add(oCategoryViewModel);
                }
            }
            return lstCategoryViewModel;
        }

        public async Task<CategoryViewModel> GetCategoryByID(int ID, int companyId)
        {
            if (ID > 0)
            {
                List<Category> lstCategory = await db.Categories.Where(w => w.CompanyID == companyId && w.CategoryID == ID && w.IsActive == true).ToListAsync();
                if (lstCategory != null)
                {
                    Category oCategory = lstCategory.FirstOrDefault();
                    CategoryViewModel oCategoryViewModel = new CategoryViewModel();
                    oCategoryViewModel.CategoryID = oCategory.CategoryID;
                    oCategoryViewModel.CategoryName = oCategory.CategoryName;
                    oCategoryViewModel.CategoryDescription = oCategory.CategoryDescription;
                    return oCategoryViewModel;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ProductViewModel>> GetAllProducts(int companyId)
        {
            var list = await db.Products.Where(w => w.CompanyID == companyId && w.IsActive == true).ToListAsync();
            List<ProductViewModel> lstProductViewModel = new List<ProductViewModel>();
            if (list != null && list.Count() > 0)
            {
                foreach (Product item in list)
                {
                    ProductViewModel oProductViewModel = new ProductViewModel();
                    oProductViewModel.ProductId = item.ProductId;
                    oProductViewModel.ProductName = item.ProductName;
                    oProductViewModel.ProductCode = item.ProductCode;
                    oProductViewModel.ProductDescription = item.ProductDescription;
                    oProductViewModel.CategoryName = CatRepo.getCategory(companyId).Where(w => w.CategoryID == item.CategoryID && w.IsActive == true).Select(s => s.CategoryName).FirstOrDefault();

                    oProductViewModel.ProductCost = item.ProductCost.ToString();
                    oProductViewModel.ProductDiscCost = item.ProductDiscCost.ToString();
                    oProductViewModel.ProductQty = Convert.ToInt32(item.ProductQty);
                    oProductViewModel.Image = ConfigurationManager.AppSettings["Product"].ToString() + "/" + item.Image;
                    oProductViewModel.SequenceNo = Convert.ToInt32(item.SequenceNo);

                    lstProductViewModel.Add(oProductViewModel);
                }
            }
            return lstProductViewModel;
        }

        public async Task<List<ProductViewModel>> GetAllProductsByCategoryID(int companyId, int CategoryID)
        {
            if (CategoryID > 0)
            {
                var list = await db.Products.Where(w => w.CompanyID == companyId && w.CategoryID == CategoryID && w.IsActive == true).ToListAsync();
                List<ProductViewModel> lstProductViewModel = new List<ProductViewModel>();
                if (list != null && list.Count() > 0)
                {
                    foreach (Product item in list)
                    {
                        ProductViewModel oProductViewModel = new ProductViewModel();
                        oProductViewModel.ProductId = item.ProductId;
                        oProductViewModel.ProductName = item.ProductName;
                        oProductViewModel.ProductCode = item.ProductCode;
                        oProductViewModel.ProductDescription = item.ProductDescription;
                        oProductViewModel.CategoryName = CatRepo.getCategory(companyId).Where(w => w.CategoryID == item.CategoryID && w.IsActive == true).Select(s => s.CategoryName).FirstOrDefault();
                        oProductViewModel.ProductCost = item.ProductCost.ToString();
                        oProductViewModel.ProductDiscCost = item.ProductDiscCost.ToString();
                        oProductViewModel.ProductQty = Convert.ToInt32(item.ProductQty);
                        oProductViewModel.Image = ConfigurationManager.AppSettings["Product"].ToString() + "/" + item.Image;
                        oProductViewModel.SequenceNo = Convert.ToInt32(item.SequenceNo);

                        lstProductViewModel.Add(oProductViewModel);
                    }
                }
                return lstProductViewModel;
            }
            else
            {
                return null;
            }

        }

        public async Task<ProductViewModel> GetProductByID(int ID, int companyId)
        {
            if (ID > 0)
            {
                List<Product> lstProduct = await db.Products.Where(w => w.CompanyID == companyId && w.ProductId == ID && w.IsActive == true).ToListAsync();

                if (lstProduct != null && lstProduct.Count() > 0)
                {
                    Product oProduct = lstProduct.FirstOrDefault();
                    ProductViewModel oProductViewModel = new ProductViewModel();
                    oProductViewModel.ProductId = oProduct.ProductId;
                    oProductViewModel.ProductName = oProduct.ProductName;
                    oProductViewModel.ProductCode = oProduct.ProductCode;
                    oProductViewModel.ProductDescription = oProduct.ProductDescription;
                    oProductViewModel.CategoryName = CatRepo.getCategory(companyId).Where(w => w.CategoryID == oProduct.CategoryID && w.IsActive == true).Select(s => s.CategoryName).FirstOrDefault();
                    oProductViewModel.ProductCost = oProduct.ProductCost.ToString();
                    oProductViewModel.ProductDiscCost = oProduct.ProductDiscCost.ToString();
                    oProductViewModel.ProductQty = Convert.ToInt32(oProduct.ProductQty);
                    oProductViewModel.Image = ConfigurationManager.AppSettings["Product"].ToString() + "/" + oProduct.Image;
                    oProductViewModel.SequenceNo = Convert.ToInt32(oProduct.SequenceNo);
                    return oProductViewModel;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
