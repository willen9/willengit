using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IProductCategoryDAL
    {
        List<ProductCategory> GetProductCategoryALL(ProductCategory productCategory, int Page);

        List<ProductCategory> SearchProductCategory(string col, string condition, string value);

        bool IsProductTypeId(string ProductCategoryType, string ProductTypeId);

        bool AddProductCategory(ProductCategory productCategory,out string msg);

        bool UpdateProductCategory(ProductCategory productCategory, out string msg);

        void DeleteProductCategory(string ProductCategoryType, string ProductTypeId);

        ProductCategory GetProductCategory(string CategoryType, string ProductTypeId);

        string GetProductType(string CategoryType, string ProductTypeId);

        bool ImportFile(string path);
    }
}