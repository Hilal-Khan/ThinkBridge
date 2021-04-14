using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopBridge.DAL
{
    public class CategoryRepository
    {
        private ShopBridgeEntities db = new ShopBridgeEntities();

        public CategoryRepository()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Database.CommandTimeout = 180;
        }

        public List<Category> getCategory(int companyId, string type = "Category")
        {
            List<Category> oListCategory = new List<Category>();
            oListCategory = db.Categories.Where(o => o.CompanyID == companyId && o.IsActive == true).ToList();
            return oListCategory;
        }
        
        public List<Category> getCategory(int ID, int companyId)
        {
            List<Category> oListCategory = new List<Category>();
            oListCategory = db.Categories.Where(o => o.CompanyID == companyId && o.IsActive == true && o.CategoryID == ID).ToList();
            return oListCategory;
        }

        public int addCategory(Category oCategory)
        {
            db.Categories.Add(oCategory);
            db.SaveChanges();
            return oCategory.CategoryID;
        }

        public int updateCategory(Category Category)
        {

            db.Entry(Category).State = EntityState.Modified;
            db.SaveChanges();
            return Category.CategoryID;
        }


    }
}