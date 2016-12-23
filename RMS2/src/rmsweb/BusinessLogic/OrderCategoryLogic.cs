using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class OrderCategoryLogic
    {
        IOrderCategoryDAL objDAL = new OrderCategoryDAL();

        public bool AddOrderCategory(OrderCategory orderCategory, out string msg)
        {
            return objDAL.AddOrderCategory(orderCategory, out msg);
        }

        public List<OrderCategory> GetOrderCategory(string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.GetOrderCategory(Col, Condition, conditionValue, Page);
        }

        public bool DelOrderCategory(string OrderType,string OrderCategory,out string msg)
        {
            return objDAL.DelOrderCategory(OrderType, OrderCategory,out msg);
        }

        public OrderCategory GetOrderCategoryInfo(string OrderType)
        {
            return objDAL.GetOrderCategoryInfo(OrderType);
        }

        public bool IsOrderCategory(string OrderType)
        {
            return objDAL.IsOrderCategory(OrderType);
        }

        public bool UpdataOrderCategory(OrderCategory orderCategory)
        {
            return objDAL.UpdataOrderCategory(orderCategory);
        }

        public List<OrderCategory> SearchOrderCategory(string col, string condition, string value)
        {
            return objDAL.SearchOrderCategory(col, condition, value);
        }

        public OrderCategory GetOrderTypeName(string OrderType, string OrderCategory)
        {
            return objDAL.GetOrderTypeName(OrderType, OrderCategory);
        }
    }
}