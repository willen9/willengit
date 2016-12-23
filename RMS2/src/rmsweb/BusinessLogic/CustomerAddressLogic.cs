using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class CustomerAddressLogic
    {
        ICustomerAddressDAL objDAL = new CustomerAddressDAL();

        public List<CustomerAddress> GetCustomerAddress(string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.GetCustomerAddress(Col, Condition, conditionValue, Page);
        }

        public CustomerAddress GetCustomerAddressInfo(string CustomerId, string AddressSName)
        {
            return objDAL.GetCustomerAddressInfo(CustomerId, AddressSName);
        }
    }
}