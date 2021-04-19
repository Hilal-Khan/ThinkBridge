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
    public class ProductRepository
    {
        private ShopBridgeEntities db = new ShopBridgeEntities();

        public ProductRepository()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Database.CommandTimeout = 180;
        }

        public List<Product> getProduct(int companyId)
        {
            List<Product> oListProduct = new List<Product>();
            oListProduct = db.Products.Where(o => o.CompanyID == companyId && o.IsActive == true).ToList();
            return oListProduct;
        }
        
        public List<Product> getProduct(int ID, int companyId)
        {
            List<Product> oListProduct = new List<Product>();
            oListProduct = db.Products.Where(o => o.CompanyID == companyId && o.IsActive == true && o.ProductId == ID).ToList();
            return oListProduct;
        }

        public int addProduct(Product oProduct)
        {
            db.Products.Add(oProduct);
            db.SaveChanges();
            return oProduct.ProductId;
        }

        public int updateProduct(Product Product)
        {

            db.Entry(Product).State = EntityState.Modified;
            db.SaveChanges();
            return Product.ProductId;
        }
    }
}