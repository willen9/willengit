using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IBrandDAL
    {
        string GetBrandName(string BrandId);

        bool IsModuleId(string brandId);

        List<Brand> GetBrand(Brand brand, string Col, string Condition, string conditionValue, int Page);

        bool AddBrand(Brand brand, out string msg);

        bool UpdateBrand(Brand brand);

        void DeleteBrand(string BrandId);

        Brand GetBrand(string BrandId);

        List<Brand> SearchBrand(string col, string condition, string value);
    }
}