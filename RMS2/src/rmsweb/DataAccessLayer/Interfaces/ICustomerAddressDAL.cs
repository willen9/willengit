using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface ICustomerAddressDAL
    {
        List<CustomerAddress> GetCustomerAddress(string Col, string Condition, string conditionValue, int Page);

        CustomerAddress GetCustomerAddressInfo(string CustomerId, string AddressSName);
    }
}