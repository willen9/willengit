using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IVendorContactDAL
    {
        List<VendorContact> GetVendorContact(string Col, string Condition, string conditionValue, int Page);
    }
}