using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopBridge.Models;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Text;

namespace ShopBridge.HelperUtility
{
    public class FileUploader
    {
        public string uploadFile(HttpPostedFileBase oImage, string expectedFileName, ConstantEnums.FileUploaderPath oUploaderPath, string randomStartWith = "Random_File", int thumbnilWidth = 100, int thumbnilHeight = 100)
        {
            string fileName = "";
            if (oImage != null && oImage.ContentLength > 0)
            {
                //Original Image
                string basePath = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings[oUploaderPath.ToString()]);
                Directory.CreateDirectory(basePath);
                fileName = Path.GetFileName(oImage.FileName);
                //fileName = (expectedFileName + "." + fileName.Split('.').Last()).RemoveSpecialCharacters();
                string path = basePath + "\\" + fileName;
                if (System.IO.File.Exists(path))
                {
                    //File with same name already exist in respective folder
                    fileName = (randomStartWith + Path.GetRandomFileName() + DateTime.Now.ToString("ddMMyyhhmmss") + "." + fileName); //fileName.Split('.').Last()).RemoveSpecialCharacters();
                    path = basePath + "\\" + fileName;
                }
                oImage.SaveAs(path);
                
            }
            return fileName;
        }

        public void deleteFile(string fileName, ConstantEnums.FileUploaderPath oUploaderPath)
        {
            string basePath = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings[oUploaderPath.ToString()]);
            string path = basePath + "\\" + fileName;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        
    }
}