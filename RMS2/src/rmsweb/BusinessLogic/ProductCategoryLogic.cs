using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class ProductCategoryLogic
    {
        IProductCategoryDAL objDal = new ProductCategoryDAL();

        public List<ProductCategory> GetProductCategoryALL(ProductCategory productCategory,int Page)
        {
            return objDal.GetProductCategoryALL(productCategory, Page);
        }

        public List<ProductCategory> SearchProductCategory(string col, string condition, string value)
        {
            return objDal.SearchProductCategory(col, condition, value);
        }

        public bool IsProductTypeId(string ProductCategoryType, string ProductTypeId)
        {
            return objDal.IsProductTypeId(ProductCategoryType, ProductTypeId);
        }

        public bool AddProductCategory(ProductCategory productCategory,out string msg)
        {
            return objDal.AddProductCategory(productCategory, out msg);
        }

        public bool UpdateProductCategory(ProductCategory productCategory,out string msg)
        {
            return objDal.UpdateProductCategory(productCategory,out msg);
        }

        public void DeleteProductCategory(string ProductCategoryType, string ProductTypeId)
        {
            objDal.DeleteProductCategory(ProductCategoryType, ProductTypeId);
        }

        public ProductCategory GetProductCategory(string CategoryType,string ProductTypeId)
        {
            return objDal.GetProductCategory(CategoryType, ProductTypeId);
        }

        public string GetProductType(string CategoryType,string ProductTypeId)
        {
            return objDal.GetProductType(CategoryType, ProductTypeId); 
        }

        public bool ImportFile(string path)
        {
            return objDal.ImportFile(path);
        }
    }
}