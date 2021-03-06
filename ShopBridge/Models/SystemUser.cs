//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopBridge.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SystemUser
    {
        public int UserId { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> EditedOn { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> EditedBy { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public string ExCol1Varchar { get; set; }
        public string ExCol2Varchar { get; set; }
        public string ExCol3Varchar { get; set; }
        public string ExCol4Varchar { get; set; }
        public Nullable<int> ExCol1Int { get; set; }
        public Nullable<int> ExCol2Int { get; set; }
        public Nullable<int> ExCol3Int { get; set; }
        public Nullable<int> ExCol4Int { get; set; }
        public Nullable<bool> ExCol1Bit { get; set; }
        public Nullable<bool> ExCol2Bit { get; set; }
        public Nullable<bool> ExCol3Bit { get; set; }
        public Nullable<bool> ExCol4Bit { get; set; }
        public Nullable<decimal> ExCol1Decimal { get; set; }
        public Nullable<decimal> ExCol2Decimal { get; set; }
    }
}
