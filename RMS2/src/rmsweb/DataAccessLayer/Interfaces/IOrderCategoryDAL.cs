using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderCategoryDAL
    {
        bool AddOrderCategory(OrderCategory orderCategory, out string msg);

        List<OrderCategory> GetOrderCategory( string Col, string Condition, string conditionValue, int Page);

        bool DelOrderCategory(string OrderType, string OrderCategory, out string msg);

        OrderCategory GetOrderCategoryInfo(string OrderType);

        bool IsOrderCategory(string OrderType);

        bool UpdataOrderCategory(OrderCategory orderCategory);

        List<OrderCategory> SearchOrderCategory(string col, string condition, string value);

        OrderCategory GetOrderTypeName(string OrderType, string OrderCategory);
    }
}