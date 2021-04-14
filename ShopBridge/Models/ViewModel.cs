using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBridge.Models
{
    public class ViewModel
    {
    }

    public class CategoryViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }

    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryName { get; set; }
        public string ProductCost { get; set; }
        public string ProductDiscCost { get; set; }
        public int ProductQty { get; set; }
        public string Image { get; set; }
        public int SequenceNo { get; set; }
    }

    


}