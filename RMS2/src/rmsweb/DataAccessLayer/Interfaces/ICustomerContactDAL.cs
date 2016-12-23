using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ICustomerContactDAL
    {
        List<CustomerContact> GetCustomerContact(string Col, string Condition, string conditionValue, int Page);

        CustomerContact GetCustomerContactInfo(string CustomerId, string Contact);
    }
}