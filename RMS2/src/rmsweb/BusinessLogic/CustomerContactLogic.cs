using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class CustomerContactLogic
    {
        ICustomerContactDAL objDAL = new CustomerContactDAL();

        public List<CustomerContact> GetCustomerContact( string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.GetCustomerContact(Col, Condition, conditionValue, Page);
        }
        public CustomerContact GetCustomerContactInfo(string CustomerId, string Contact)
        {
            return objDAL.GetCustomerContactInfo(CustomerId, Contact);
        }
    }
}