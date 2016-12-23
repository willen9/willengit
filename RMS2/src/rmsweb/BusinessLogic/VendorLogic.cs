using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;

namespace BusinessLogic
{
    public class VendorLogic
    {
        public IVendor objDal = new VendorDAL();

        public DataTable SelectVendor(Vendor vendor,int Page,string condition1,string condition2)
        {
            return objDal.SelectVendor(vendor, Page,condition1,condition2);
        }

        public string UpdateVendor(Vendor vendor, string AddressId, string txtAddress1, string txtAddress2, string txtAddressContact, string txtAddressTelNo, string txtAddressFaxNo, string txtAddressRemark)
        {
            return objDal.UpdateVendor(vendor, AddressId, txtAddress1, txtAddress2, txtAddressContact, txtAddressTelNo, txtAddressFaxNo, txtAddressRemark);
        }
        public bool IsModuleId(string vendorid)
        {
            return objDal.IsModuleId(vendorid);
        }
        public bool DeleteVendor(Vendor vendor)
        {
            return objDal.DeleteVendor(vendor);
        }

        public string InsertVendor(Vendor vendor, string AddressId, string txtAddress1, string txtAddress2, string txtAddressContact, string txtAddressTelNo, string txtAddressFaxNo, string txtAddressRemark)
        {
            return objDal.InsertVendor(vendor,AddressId, txtAddress1, txtAddress2, txtAddressContact, txtAddressTelNo, txtAddressFaxNo, txtAddressRemark);
        }

        public DataTable SearchDetailVendor(string col, string condition, string value)
        {
            return objDal.SearchDetailVendor(col, condition, value);
        }

        public string GetVendorName(string VendorId)
        {
            return objDal.GetVendorName(VendorId);
        }

        public bool ImportFile(string path, string type, out string msg)
        {
            return objDal.ImportFile(path, type, out msg);
        }

        public DataTable GetAddressInfo(VendorAddress vendorAddress)
        {
            return objDal.GetAddressInfo(vendorAddress);
        }

        public DataTable GetContactInfo(VendorContact vendorContact)
        {
            return objDal.GetContactInfo(vendorContact);
        }

        public bool SaveNewContact(VendorContact vendorContact)
        {
            return objDal.SaveNewContact(vendorContact);
        }

        public bool DeleteContact(VendorContact vendorContact)
        {
            return objDal.DeleteContact(vendorContact);
        }

        public bool SaveContact(VendorContact vendorContact)
        {
            return objDal.SaveContact(vendorContact);
        }

        public bool DeleteAddress(VendorAddress vendorAddress)
        {
            return objDal.DeleteAddress(vendorAddress);
        }

        public bool SaveAddress(VendorAddress vendorAddress)
        {
            return objDal.SaveAddress(vendorAddress);
        }

        public bool SaveNewAddress(VendorAddress vendorAddress)
        {
            return objDal.SaveNewAddress(vendorAddress);
        }

        public DataTable SelectVendorList(string Col, string Condition, string conditionValue, int Page)
        {
            return objDal.SelectVendorList(Col, Condition, conditionValue, Page);
        }
    }
}