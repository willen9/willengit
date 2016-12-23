using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IProductDAL
    {
        List<Product> GetProduct(Product product, string Col, string Condition, string conditionValue, int Page);

        bool IsProductNo(string ProductNo);

        bool AddProduct(Product product, out string msg);

        bool UpdateProduct(Product product, out string msg);

        void DeleteProduct(string ProductNo);

        Product GetProductInfo(string ProductNo);

        bool ImportFile(string path);

        List<Product> SearchProductCategory(string col, string condition, string value);
    }
}