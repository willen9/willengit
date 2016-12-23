using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class BrandLogic
    {
        IBrandDAL objDAL = new BrandDAL();

        public string GetBrandName(string BrandId)
        {
            return objDAL.GetBrandName(BrandId);
        }

        public bool IsModuleId(string brandId)
        {
            return objDAL.IsModuleId(brandId);
        }

        public List<Brand> GetBrand(Brand brand, string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.GetBrand(brand, Col, Condition, conditionValue, Page);
        }

        public bool AddBrand(Brand brand, out string msg)
        {
            return objDAL.AddBrand(brand, out msg);
        }

        public bool UpdateBrand(Brand brand)
        {
            return objDAL.UpdateBrand(brand);
        }

        public void DeleteBrand(string BrandId)
        {
            objDAL.DeleteBrand(BrandId);
        }

        public Brand GetBrand(string BrandId)
        {
            return objDAL.GetBrand(BrandId);
        }

        public List<Brand> SearchBrand(string col, string condition, string value)
        {
            return objDAL.SearchBrand(col, condition, value);
        }
    }
}