using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class ProductLogic
    {
        IProductDAL objDal = new ProductDAL();

        public List<Product> GetProduct(Product product, string Col, string Condition, string conditionValue, int Page)
        {
            return objDal.GetProduct(product, Col, Condition, conditionValue, Page);
        }

        public bool IsProductNo(string ProductNo)
        {
            return objDal.IsProductNo(ProductNo);
        }

        public bool AddProduct(Product product, out string msg)
        {
            return objDal.AddProduct(product, out msg);
        }

        public bool UpdateProduct(Product product, out string msg)
        {
            return objDal.UpdateProduct(product, out msg);
        }

        public void DeleteProduct(string ProductNo)
        {
            objDal.DeleteProduct(ProductNo);
        }

        public Product GetProductInfo(string ProductNo)
        {
            return objDal.GetProductInfo(ProductNo);
        }

        public bool ImportFile(string path)
        {
            return objDal.ImportFile(path);
        }

        public List<Product> SearchProductCategory(string col, string condition, string value)
        {
            return objDal.SearchProductCategory(col, condition, value);
        }
    }
}