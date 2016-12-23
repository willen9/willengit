using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class VendorContactLogic
    {
        IVendorContactDAL objDAL = new VendorContactDAL();

        public List<VendorContact> GetVendorContact( string Col, string Condition, string conditionValue, int Page)
        {
            return objDAL.GetVendorContact(Col, Condition, conditionValue, Page);
        }
    }
}