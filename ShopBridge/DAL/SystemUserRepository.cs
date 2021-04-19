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
    public class SystemUserRepository
    {
        private ShopBridgeEntities db = new ShopBridgeEntities();

        public SystemUserRepository()
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Database.CommandTimeout = 180;
        }

        public List<SystemUser> getSystemUser(int companyId)
        {            
            List<SystemUser> oListSystemUser = new List<SystemUser>();
            oListSystemUser = db.SystemUsers.Where(o =>o.CompanyID==companyId && o.IsActive == true).ToList();
            return oListSystemUser;
        }

        public List<SystemUser> getSystemUserLIst(IEnumerable<int> lstID, int companyId)
        {
            List<SystemUser> oListSystemUserDetail = new List<SystemUser>();
            oListSystemUserDetail = db.SystemUsers.Where(o => o.CompanyID == companyId && lstID.Any(p => p == o.UserId)).ToList();
            return oListSystemUserDetail;
        }
        
        public List<SystemUser> getSystemUser(int ID, int companyId)
        {
           
            List<SystemUser> oListSystemUser = new List<SystemUser>();
            oListSystemUser = db.SystemUsers.Where(o => o.CompanyID == companyId && o.IsActive == true && o.UserId == ID).ToList();
            return oListSystemUser;
        }

        public List<SystemUser> getSystemUser(string loginId, string pw)
        {
           
            List<SystemUser> oListSystemUser = new List<SystemUser>();
            oListSystemUser = db.SystemUsers.Where(o => o.IsActive == true && o.LoginID == loginId && o.Password == pw).ToList();
            return oListSystemUser;
        }

        public List<SystemUser> getSystemUser(string loginId)
        {

            List<SystemUser> oListSystemUser = new List<SystemUser>();
            oListSystemUser = db.SystemUsers.Where(o => o.IsActive == true && o.LoginID == loginId).ToList();
            return oListSystemUser;
        }

        public int addSystemUser(SystemUser oSystemUser)
        {
            db.SystemUsers.Add(oSystemUser);
            db.SaveChanges();
            return oSystemUser.UserId;
        }

        public int updateSystemUser(SystemUser SystemUser)
        {
            db.Entry(SystemUser).State = EntityState.Modified;
            db.SaveChanges();
            return SystemUser.UserId;
        }

        public SystemUser getSystemUserById(int loginId)
        {
            SystemUser oListSystemUser = new SystemUser();
            oListSystemUser = db.SystemUsers.Where(o => o.UserId == loginId && o.IsActive == true).SingleOrDefault();
            return oListSystemUser;
        }

    }
}